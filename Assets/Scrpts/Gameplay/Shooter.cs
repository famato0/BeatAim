using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    public static event Action<bool> ShotFired;

    [SerializeField] private float maxDistance = 100f;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
            Shoot();
    }

    private void Shoot()
    {
        bool hitTarget = false;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            if (hit.collider.TryGetComponent(out IHittable hittable))
            {
                hittable.OnHit();
                hitTarget = true;
            }
        }

        ShotFired?.Invoke(hitTarget);
    }
}