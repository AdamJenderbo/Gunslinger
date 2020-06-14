using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Inventory/Item/Gun")]
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
    }

    public override bool IsGun()
    {
        return true;
    }
}
