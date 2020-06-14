using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Hotkey : MonoBehaviour
{
    public List<UI_Slot> uiHotkeySlots;

    void Start()
    {
        Inventory inventory = Player.instance.GetInventory();
        Inventory.Slot[] hotkeySlots = inventory.GetHotkeySlots();
        for (int i = 0; i < uiHotkeySlots.Count; i++)
        {
            uiHotkeySlots[i].SetSlot(hotkeySlots[i]);
            hotkeySlots[i].onItemChangedCallback += Refresh;
        }
        Refresh();
    }

    void Refresh()
    {
        for (int i = 0; i < uiHotkeySlots.Count; i++)
        {
            uiHotkeySlots[i].ShowIcon();
        }
    }
}
