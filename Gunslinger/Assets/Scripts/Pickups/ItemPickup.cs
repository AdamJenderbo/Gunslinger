using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Pickup
{
    public Item item;

    private void Start()
    {
        onTriggerAction = (Player player) =>
        {
            if (player.PickUp(item))
            {
                Destroy(gameObject);
            }
        };
    }
}
