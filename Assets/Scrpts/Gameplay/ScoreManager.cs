using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public event Action Changed;

    public int Hits { get; private set; }
    public int Shots { get; private set; }
    public int Expired { get; private set; }
    public float Accuracy => Shots > 0 ? (float)Hits / Shots * 100f : 0f;

    private void OnEnable()
    {
        Shooter.ShotFired += OnShotFired;
        Target.TargetExpired += OnTargetExpired;
    }

    private void OnDisable()
    {
        Shooter.ShotFired -= OnShotFired;
        Target.TargetExpired -= OnTargetExpired;
    }

    private void OnShotFired(bool hit)
    {
        Shots++;
        if (hit) Hits++;
        Changed?.Invoke();
    }

    private void OnTargetExpired()
    {
        Expired++;
        Changed?.Invoke();
    }
}