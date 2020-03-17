using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPocket
{
    public int Amount { get; private set; }

    public MoneyPocket()
    {
        Amount = 0;
    }

    public bool SpendMoney(int amount)
    {
        if (Amount < amount)
            return false;
        Amount -= amount;
        return true;
    }

    public bool AddMoney(int amount)
    {
        Amount += amount;
        return true;
    }
}
