using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet bullet;
    private Transform firePoint;

    // ammo
    public int MaxAmmo;
    int currentAmmo;

    public bool Loaded { get { return currentAmmo > 0; } }

    private void Start()
    {
        firePoint = transform.Find("Fire Point");
        currentAmmo = 0;
    }

    public void Fire()
    {
        if(!Loaded)
        {
            Debug.LogWarning("Gun not loaded");
            // dry sound
            return;
        }
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        currentAmmo--;
        // fire sound
    }

    public void Reload(int ammo)
    {
        if(currentAmmo + ammo > MaxAmmo)
        {
            Debug.LogWarning("Tried to load to much ammo");
            currentAmmo = MaxAmmo;
            return;
        }
        currentAmmo += ammo;
    }
}
