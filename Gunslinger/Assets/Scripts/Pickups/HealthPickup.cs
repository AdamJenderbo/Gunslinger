﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public int healAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        onTriggerAction = (Player player) =>
        {
            //player.Heal(healAmount);
            Destroy(gameObject);
        };
    }
}
