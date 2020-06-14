using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Bullet bullet;
    private Gun gun;

    private Transform rightHand;

    private void Start()
    {
        rightHand = transform.Find("Aim").Find("Right Hand");
        gun = rightHand.GetComponentInChildren<Gun>();
        gun.SetBullet(bullet);
    }

    public void Shoot()
    {
        gun.Fire();
    }

    public void HoldShoot()
    {
        if(gun.gunType == Gun.GunType.MACHINE_GUN)
        {
            gun.Fire();
        }
    }

    public Gun GetGun()
    {
        return gun;
    }

    public void SetGun(Gun newGun)
    {
        Destroy(gun.gameObject);
        gun = Instantiate(newGun, rightHand).GetComponent<Gun>();
        gun.SetBullet(bullet);
    }

    public void DropGun()
    {
        Instantiate(gun.pickup, transform.position, transform.rotation);
        Destroy(gun.gameObject);
    }
}
