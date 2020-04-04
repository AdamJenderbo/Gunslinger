using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Transform firePoint;
    Bullet bulletToFire;
    public float fireDelay;
    private Cooldown shotCooldown;

    public GunPickup pickup;
    public Gun gunPrefab;

    public Item.ItemType gunType;

    public int maxAmmo;
    int ammo;


    public bool Ready { get { return shotCooldown.Done(); } }
    public bool Loaded { get { return ammo > 0; } }

    // Start is called before the first frame update
    void Start()
    {
        ammo = 0;
        shotCooldown = new Cooldown(fireDelay);
        firePoint = transform.Find("Fire Point");
    }

    // Update is called once per frame
    void Update()
    {
        if(!Ready)
            shotCooldown.Update();
    }

    public bool Fire()
    {
        if (Ready)
        {
            if (Loaded)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                shotCooldown.Reset();
                ammo--;
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public void Reload(int amount)
    {
        if (ammo + amount > maxAmmo) { Debug.LogWarning("Tried to load to much ammo"); ammo = maxAmmo; return; }
        ammo += amount;
    }

    public void Drop()
    {
        Instantiate(pickup, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void SetBullet(Bullet bullet)
    {
        bulletToFire = bullet;
    }
}
