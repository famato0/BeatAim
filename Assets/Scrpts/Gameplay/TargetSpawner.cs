using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private Target targetPrefab;
    [SerializeField] private int maxActive = 1; //TODO - In base alla difficolta' possono aumentare i target, + in base alla difficolta diminusce il tempo di lifetime dei target
    [SerializeField] private Vector2 areaCenter = new Vector2(0f, 2.5f);
    [SerializeField] private Vector2 areaSize = new Vector2(10f, 3.5f);
    [SerializeField] private float depth = 19f;
    [SerializeField] private float minDistanceBetween = 1.5f;

    private readonly List<Target> active = new List<Target>();

    private void Start()
    {
        for (int i = 0; i < maxActive; i++)
            SpawnTarget();
    }

    private void SpawnTarget()
    {
        Target target = Instantiate(targetPrefab, GetRandomPosition(), Quaternion.identity);
        target.SetColor(SettingsManager.Current.targetColor);
        target.Hit += OnTargetFinished;
        target.Expired += OnTargetFinished;
        active.Add(target);
    }

    private void OnTargetFinished(Target target)
    {
        target.Hit -= OnTargetFinished;
        target.Expired -= OnTargetFinished;
        active.Remove(target);
        SpawnTarget();
    }

    private Vector3 GetRandomPosition()
    {
        for (int attempt = 0; attempt < 20; attempt++)
        {
            Vector3 pos = new Vector3(
                areaCenter.x + Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                areaCenter.y + Random.Range(-areaSize.y / 2f, areaSize.y / 2f),
                depth);

            if (IsFarEnough(pos))
                return pos;
        }
        return new Vector3(areaCenter.x, areaCenter.y, depth);
    }

    private bool IsFarEnough(Vector3 pos)
    {
        foreach (Target t in active)
        {
            if (t != null && Vector3.Distance(t.transform.position, pos) < minDistanceBetween)
                return false;
        }
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            new Vector3(areaCenter.x, areaCenter.y, depth),
            new Vector3(areaSize.x, areaSize.y, 0.01f));
    }
}