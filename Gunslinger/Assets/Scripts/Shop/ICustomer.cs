using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICustomer
{
    bool CanAffordItem(int cost);
    bool CanBuyItem(Item item);
    void BuyItem(Item item, int cost);
}
