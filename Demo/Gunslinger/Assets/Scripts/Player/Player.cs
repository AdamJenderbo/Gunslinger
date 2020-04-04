using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D PlayerRigidbody2D;

    public PlayerView view;

    Gun gun;
    int ammo;


    private void Start()
    {
        view = GetComponent<PlayerView>();
        gun = transform.Find("Aim").transform.Find("Right Hand").GetComponentInChildren<Gun>();
        ammo = 12;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(gun.Loaded)
                gun.Fire();
            else
                ReloadGun();
            Debug.Log(ammo);
        }
    }

    public void TurnBackward()
    {
        view.TurnBackward();
    }

    public void TurnForward()
    {
        view.TurnForward();
    }

    public void TurnLeft()
    {
        view.TurnLeft();
    }

    public void TurnRight()
    {
        view.TurnRight();
    }

    private void ReloadGun()
    {
        if (ammo < gun.MaxAmmo)
        {
            gun.Reload(ammo);
            ammo = 0;
        }
        else
        {
            gun.Reload(gun.MaxAmmo);
            ammo -= gun.MaxAmmo;
        }
    }
}
