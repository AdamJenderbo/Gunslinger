using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum GunType
    {
        REVOLVER, RIFLE
    }

    public GunType gunType;


    Transform firePoint;
    Bullet bulletToFire;
    public float fireDelay;
    private Cooldown shotCooldown;

    public GunPickup pickup;
    public Gun gunPrefab;

    public bool Ready { get { return shotCooldown.Done(); } }

    // Start is called before the first frame update
    void Start()
    {
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
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);

            if(gunType == GunType.RIFLE)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation).transform.Rotate(0,0, 10f);
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation).transform.Rotate(0, 0, -10f);
            }

            shotCooldown.Reset();
            return true;
        }
        return false;
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
