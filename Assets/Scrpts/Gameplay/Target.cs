using System;
using UnityEngine;

public class Target : MonoBehaviour, IHittable
{
    public event Action<Target> Hit;
    public event Action<Target> Expired;
    public static event Action TargetExpired;

    [SerializeField] private float lifetime = 2f;
    [SerializeField] private float growDuration = 0.25f;

    private float age;
    private Vector3 fullScale;
    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        fullScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    public void SetColor(Color color)
    {
        rend.material.color = color;
    }

    private void Update()
    {
        age += Time.deltaTime;

        float t = Mathf.Clamp01(age / growDuration);
        transform.localScale = fullScale * t;

        if (age >= lifetime)
        {
            Expired?.Invoke(this);
            TargetExpired?.Invoke();
            Destroy(gameObject);
        }
    }

    public void OnHit()
    {
        Hit?.Invoke(this);
        Destroy(gameObject);
    }
}