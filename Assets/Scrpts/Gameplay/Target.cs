using System;
using UnityEngine;

public class Target : MonoBehaviour, IHittable
{
    public event Action<Target> Hit;
    public event Action<Target> Expired;   // scaduto nel tempo
    public event Action<Target> Escaped;   // uscito dall'area senza rimbalzo
    public static event Action TargetExpired;

    [SerializeField] private float growDuration = 0.25f;

    private float lifetime;   // <= 0 significa "non scade mai"
    private float age;
    private Vector3 fullScale;
    private Renderer rend;

    private bool moving;
    private bool bounce;
    private Vector2 velocity;
    private Rect area;

    public Vector2 Position2D => transform.position;
    public Vector2 Velocity => velocity;
    private float Radius => fullScale.x * 0.5f;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        fullScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    public void SetColor(Color color) => rend.material.color = color;

    public static void RefreshAllColors(Color color)
    {
        Target[] targets = FindObjectsByType<Target>(FindObjectsSortMode.None);

        foreach (Target target in targets)
        {
            target.SetColor(color);
        }
    }

    public void SetLifetime(float seconds) => lifetime = seconds;

    public void SetMotion(Vector2 direction, float speed, bool bounceOnWalls, Rect movementArea)
    {
        moving = true;
        bounce = bounceOnWalls;
        area = movementArea;
        velocity = direction.normalized * speed;
    }

    public void Reverse() => velocity = -velocity;

    private void Update()
    {
        age += Time.deltaTime;
        transform.localScale = fullScale * Mathf.Clamp01(age / growDuration);

        if (moving && !MoveStep())
            return; // uscito dall'area e distrutto

        if (lifetime > 0f && age >= lifetime)
        {
            Expired?.Invoke(this);
            TargetExpired?.Invoke();
            Destroy(gameObject);
        }
    }

    // Ritorna false se il target è uscito ed è stato distrutto
    private bool MoveStep()
    {
        Vector3 p = transform.position;
        p.x += velocity.x * Time.deltaTime;
        p.y += velocity.y * Time.deltaTime;

        if (bounce)
        {
            if (p.x < area.xMin + Radius || p.x > area.xMax - Radius)
            {
                velocity.x = -velocity.x;
                p.x = Mathf.Clamp(p.x, area.xMin + Radius, area.xMax - Radius);
            }

            if (p.y < area.yMin + Radius || p.y > area.yMax - Radius)
            {
                velocity.y = -velocity.y;
                p.y = Mathf.Clamp(p.y, area.yMin + Radius, area.yMax - Radius);
            }
        }
        else if (p.x < area.xMin - Radius || p.x > area.xMax + Radius ||
                 p.y < area.yMin - Radius || p.y > area.yMax + Radius)
        {
            Escaped?.Invoke(this);
            Destroy(gameObject);
            return false;
        }

        transform.position = p;
        return true;
    }

    public void OnHit()
    {
        Hit?.Invoke(this);
        Destroy(gameObject);
    }
}