using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : ItemPickup
{
    public GunItem gunItem;

    private void Start()
    {
        onTriggerAction = (Player player) =>
        {
            if (player.PickUp(gunItem))
            {
                Destroy(gameObject);
            }
        };
    }
}