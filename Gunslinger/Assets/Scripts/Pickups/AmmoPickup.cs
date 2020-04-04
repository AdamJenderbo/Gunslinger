using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup
{
    public int ammo;

    private void Start()
    {
        onTriggerAction = (Player player) =>
        {
            player.AddAmmo(ammo);
            Destroy(gameObject);
        };
    }
}
