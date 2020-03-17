using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/MedicinItem")]
public class MedicinItem : Item
{
    public int healAmount;

    public override void Use()
    {
        Player.instance.Heal(healAmount);
    }
}
