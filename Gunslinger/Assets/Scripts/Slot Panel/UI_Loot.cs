using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Loot : UI_SlotPanel
{
    Loot loot;

    private void Awake()
    {
        Setup();
        colums = 4;
        rows = 2;
        DrawPanel();
    }

    public void SetLoot(Loot loot)
    {
        this.loot = loot;
    }

    public void ShowLoot()
    {
        SetSlots(loot.slots);
        DrawIcons();
    }

    public void HideLoot()
    {

    }
}
