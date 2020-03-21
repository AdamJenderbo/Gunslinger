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
        SetHeaderText("Loot");
    }

    public void SetLoot(Loot loot)
    {
        this.loot = loot;
    }

    public void ShowLoot()
    {
        SetSlots(loot.slots);
        DrawIcons();
        for(int row = 0; row < rows; row++)
        {
            for(int col = 0; col < colums; col++)
            {
                Inventory.Slot slot = loot.slots[col + (colums * row)];
                SetButtonAction(row, col, () =>
                {
                    loot.SlotClicked(slot);
                    DrawIcons();
                });
            }
        }
    }

    public void HideLoot()
    {

    }
}
