using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : Pickup
{
    public GunItem gunItem;

    private void Start()
    {
        onTriggerAction = (Player player) =>
        {
            player.PickUpGun(gunItem);
            Destroy(gameObject);
        };
    }

    protected override void Update()
    {
        if (playerEntered)
        {
            if(Input.GetKeyDown(KeyCode.E))
                onTriggerAction(player);
        }
    }
}