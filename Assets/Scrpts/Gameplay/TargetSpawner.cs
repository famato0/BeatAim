using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private Target targetPrefab;

    // BackWall: i target si muovono su tutta la sua superficie
    [SerializeField] private Renderer wall;

    // Zona dove nascono i target all'avvio e a ogni sostituzione
    [SerializeField] private Vector2 areaCenter = new Vector2(0f, 2.5f);
    [SerializeField] private Vector2 areaSize = new Vector2(10f, 3.5f);

    [SerializeField] private float depth = 19f;
    [SerializeField] private float targetRadius = 0.5f;

    private ModeConfig config;
    private readonly List<Target> active = new List<Target>();

    private Rect SpawnArea => new Rect(areaCenter - areaSize / 2f, areaSize);

    // Se il muro non è assegnato, ripiega sulla zona di spawn
    private Rect MovementArea
    {
        get
        {
            if (wall == null) return SpawnArea;

            Bounds b = wall.bounds;
            return Rect.MinMaxRect(b.min.x, b.min.y, b.max.x, b.max.y);
        }
    }

    private bool IsMoving =>
        config.motion.patterns != null &&
        config.motion.patterns.Count > 0;

    private void Start()
    {
        config = ModeLoader.Selected ?? new ModeConfig();

        int count = Mathf.Clamp(config.maxTargets, 1, 5);
        for (int i = 0; i < count; i++)
            SpawnTarget();
    }

    private void Update()
    {
        if (IsMoving)
            ResolveOverlaps();
    }

    private void SpawnTarget()
    {
        Target target = Instantiate(targetPrefab, GetFreePosition(), Quaternion.identity);
        target.SetColor(SettingsManager.Current.targetColor);

        if (config.despawn.enabled)
            target.SetLifetime(config.despawn.lifetime);

        if (IsMoving)
            target.SetMotion(RandomDirection(), config.motion.speed, config.motion.bounce, MovementArea);

        target.Hit += OnTargetFinished;
        target.Expired += OnTargetFinished;
        target.Escaped += OnTargetFinished;
        active.Add(target);
    }

    // Ogni target sceglie a caso una direzione compatibile con il pattern
    private Vector2 RandomDirection()
    {
        List<string> patterns = config.motion.patterns;

        if (patterns == null || patterns.Count == 0)
            return Vector2.zero;

        string pattern = patterns[Random.Range(0, patterns.Count)];

        switch (pattern)
        {
            case "Horizontal": //TODO - Mettere ENUM
                return Random.value < 0.5f
                    ? Vector2.left
                    : Vector2.right;

            case "Vertical":
                return Random.value < 0.5f
                    ? Vector2.up
                    : Vector2.down;

            case "Diagonal":
                float x = Random.value < 0.5f ? -1f : 1f;
                float y = Random.value < 0.5f ? -1f : 1f;
                return new Vector2(x, y);

            default:
                return Vector2.zero;
        }
    }


    private void OnTargetFinished(Target target)
    {
        target.Hit -= OnTargetFinished;
        target.Expired -= OnTargetFinished;
        target.Escaped -= OnTargetFinished;
        active.Remove(target);
        SpawnTarget();
    }

    private Vector3 GetFreePosition()
    {
        Rect area = SpawnArea;
        float minDistance = targetRadius * 2.2f;

        for (int attempt = 0; attempt < 30; attempt++)
        {
            Vector3 pos = new Vector3(
                Random.Range(area.xMin + targetRadius, area.xMax - targetRadius),
                Random.Range(area.yMin + targetRadius, area.yMax - targetRadius),
                depth);

            if (IsFree(pos, minDistance))
                return pos;
        }

        return new Vector3(areaCenter.x, areaCenter.y, depth);
    }

    private bool IsFree(Vector3 pos, float minDistance)
    {
        foreach (Target t in active)
        {
            if (t != null && Vector2.Distance(t.Position2D, pos) < minDistance)
                return false;
        }
        return true;
    }

    private void ResolveOverlaps()
    {
        float minDistance = targetRadius * 2f;

        for (int i = 0; i < active.Count; i++)
        {
            for (int j = i + 1; j < active.Count; j++)
            {
                Target a = active[i];
                Target b = active[j];

                Vector2 offset = b.Position2D - a.Position2D;
                if (offset.magnitude >= minDistance) continue;

                Vector2 relativeVelocity = b.Velocity - a.Velocity;
                if (Vector2.Dot(offset, relativeVelocity) < 0f)
                {
                    a.Reverse();
                    b.Reverse();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Zona di spawn (verde)
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            new Vector3(areaCenter.x, areaCenter.y, depth),
            new Vector3(areaSize.x, areaSize.y, 0.01f));

        // Area di movimento (giallo)
        if (wall != null)
        {
            Rect m = MovementArea;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(
                new Vector3(m.center.x, m.center.y, depth),
                new Vector3(m.width, m.height, 0.01f));
        }
    }
}