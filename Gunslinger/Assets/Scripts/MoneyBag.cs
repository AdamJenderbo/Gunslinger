using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : Pickup
{
    public int money;

    // Start is called before the first frame update
    void Start()
    {
        money = Random.Range(1, 6) * 10;
    }
}
