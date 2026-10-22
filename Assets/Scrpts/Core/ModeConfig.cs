using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class ModeConfig
{
    public string name = "Freeplay";
    public string type = "freeplay"; // "freeplay" oppure "music" (in arrivo)

    [Range(1, 5)] public int maxTargets = 3;
    public MotionConfig motion = new MotionConfig();
    public DespawnConfig despawn = new DespawnConfig();
}

[Serializable]
public class MotionConfig
{
    // Lista vuota = Still
    // Valori supportati: "Horizontal", "Vertical", "Diagonal"
    public List<string> patterns = new List<string>();

    public float speed = 2f;
    public bool bounce = false;
}

[Serializable]
public class DespawnConfig
{
    public bool enabled = false;
    public float lifetime = 2f;
}