using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class ClickSoundPlayer : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float volume = 0.8f;

    private AudioSource source;
    private AudioClip clip;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 0f;
    }

    private void Start()
    {
        string path = SoundLibrary.ResolvePath(
            SettingsManager.Current.clickSoundPath
        );

        StartCoroutine(LoadClip(path));
    }

    private void OnEnable() => Shooter.ShotFired += OnShot;
    private void OnDisable() => Shooter.ShotFired -= OnShot;

    private void OnShot(bool hit)
    {
        if (clip != null)
            source.PlayOneShot(clip, volume);
    }

    private IEnumerator LoadClip(string path)
    {
        string url = new Uri(path).AbsoluteUri;
        AudioType type = GetAudioType(path);

        using (UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip(url, type))
        {
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
                clip = DownloadHandlerAudioClip.GetContent(req);
            else
                Debug.LogWarning($"Impossibile caricare il suono {path}: {req.error}");
        }
    }

    private static AudioType GetAudioType(string path)
    {
        switch (System.IO.Path.GetExtension(path).ToLowerInvariant())
        {
            case ".ogg": return AudioType.OGGVORBIS;
            case ".wav": return AudioType.WAV;
            default:     return AudioType.MPEG; // .mp3
        }
    }
}