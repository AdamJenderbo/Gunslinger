using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : UI_SlotPanel
{
    Inventory inventory;

    public List<UI_Slot> uiHotkeySlots;

    public UI_ItemSlot gunSlot;
    public Image gunImage;

    public bool hidden;
    public List<Sprite> slotSprites;

    private void Awake()
    {
        rows = 3;
        colums = 3;
        Setup();
        SetHeaderText("Inventory");
    }

    public void Start()
    {
        inventory = Player.instance.GetInventory();
        Inventory.Slot[] inventorySlots = inventory.GetSlots();
        List<Inventory.Slot> slots = new List<Inventory.Slot>();
        for(int i = 0; i < inventorySlots.Length - 4; i++)
        {
            slots.Add(inventorySlots[i]);
            slots[i].onItemChangedCallback += RefreshInventory;
        }
        DrawPanel();
        SetSlots(slots);
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        if (hidden)
            return;
        DrawIcons();
    }

    public void RefreshGunSlot()
    {
        gunImage.sprite = inventory.GetGun().Sprite;
    }


    public void Show()
    {
        hidden = false;
        gameObject.SetActive(true);
        RefreshInventory();
    }

    public void Hide()
    {
        hidden = true;
        gameObject.SetActive(false);
    }
}
