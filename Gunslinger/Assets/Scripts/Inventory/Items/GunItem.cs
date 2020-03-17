using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : Item
{
    public Gun gun;

    public override bool Stackable { get { return false; } }

    public override void Use()
    {
        if(gun == null)
        {
            Debug.Log("Gun is null");
        }
        Player.instance.SwitchGun(gun);
    }

    public override bool IsGun()
    {
        return true;
    }
}
