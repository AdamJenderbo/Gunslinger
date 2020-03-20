using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Loot : UI_SlotPanel
{
    Loot loot;

    private void Awake()
    {
        colums = 4;
        rows = 2;
        DrawPanel();
    }

    public void SetLoot(Loot loot)
    {

    }

    public void ShowLoot()
    {
    }

    public void HideLoot()
    {

    }
}
