using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : Pickup
{
    private int money;

    void Start()
    {
        money = Random.Range(1, 6) * 100;
        onTriggerAction = (Player player) =>
        {
            player.AddMoney(money);
            Destroy(gameObject);
        };
    }
}
