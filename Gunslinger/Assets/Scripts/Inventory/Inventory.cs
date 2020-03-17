using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public int Space { get; private set; }

    public int Count
    {
        get
        {
            int count = 0;
            foreach (Slot slot in Slots)
            {
                if (!slot.Empty)
                    count++;
            }
            return count;
        }
    }
    public bool Full { get { return Count == Space; } }
    public bool Empty { get { return Count == 0; } }

    private Slot gunSlot;

    private Slot[] hotkeySlots;

    private Slot[] Slots;

    public Inventory(int space)
    {
        Space = space;
        Slots = new Slot[space];
        for(int i = 0; i < Slots.Length; i++)
            Slots[i] = new Slot();
        gunSlot = new Slot();
        hotkeySlots = new Slot[4];
        for(int i = 0; i < hotkeySlots.Length; i++)
        {
            hotkeySlots[i] = new Slot();
        }
    }

    public bool AddItem(Item item)
    {
        Slot slot;
        if (item.Stackable)
        {
            slot = FindSlotWithItem(item);

            if (slot != null)
            {
                slot.AddItems(1);
                UpdateUI();
                return true;
            }
        }

        if(Full)
        {
            return false;
        }

        slot = FindEmptySlot();
        if (slot == null)
        {
            Debug.LogError("Inventory.Full does not work!");
            return false;
        }

        slot.SetItem(item);

        UpdateUI();

        return true;
    }

    public void AddItem(Item item, Slot Slot)
    {
        Slot.Clear();
        Slot.SetItem(item);

        UpdateUI();
    }

    public void MoveItem(Item item, Slot slot)
    {
        /*
        if(!items.Contains(item))
        {
            Debug.Log("item not in inventory");
            return;
        }
        */

        FindSlotWithItem(item).Clear();
        slot.SetItem(item);

        UpdateUI();
    }

    public void RemoveItem(Item item)
    {
        Slot slot = FindSlotWithItem(item);

        if (slot == null)
            return; // item does not exist in inventory

        if (item.Stackable)
            slot.RemoveItems(1); // remove one item
        else
            slot.Clear();

        UpdateUI();
    }

    public void SwitchSlots(Slot slot1, Slot slot2)
    {
        Item tmpItem = slot1.Item;
        int tmpAmount = slot1.Amount;
        slot1.SetItem(slot2.Item);
        //slot1.Amount = slot2.Amount;
    }
  
    public void UseItem(Item item)
    {
        item.Use();
        RemoveItem(item);
    }

    public void UseHotKey(int number)
    {
        if (Slots[number].Empty)
            return;
        UseItem(Slots[number].Item);
    }

    public Slot[] GetSlots()
    {
        return Slots;
    }

    public void SetGun(Item item)
    {
        gunSlot.SetItem(item);
    }

    public Item GetGun()
    {
        return gunSlot.Item;
    }

    public void SetHotkeySlot(Item item, Slot hotkeySlot)
    {
        for(int i = 0; i < hotkeySlots.Length; i++)
        {
            if (hotkeySlot == hotkeySlots[i])
                hotkeySlots[i].SetItem(item);
        }
    }

    public Slot GetHotkeySlot(int index)
    {
        return hotkeySlots[index];
    }

    public Slot[] GetHotkeySlots()
    {
        return hotkeySlots;
    }

    private Slot FindSlotWithItem(Item item)
    {
        foreach(Slot slot in Slots)
        {
            if (slot.Item == item)
                return slot;
        }
        Debug.Log("No slot with item!");
        return null;
    }

    public Slot FindEmptySlot()
    {
        foreach (Slot Slot in Slots)
        {
            if (Slot.Empty)
            {
                return Slot;
            }
        }
        Debug.LogError("Cannot find an empty inventory slot!");
        return null;
    }

    private void UpdateUI()
    {
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    [Serializable]
    public class Slot
    {
        [SerializeField]
        public Item Item;
        public int Amount;
        public bool Empty { get { return Item == null; } }

        public Slot() { }
        
        public void SetItem(Item item)
        {
            Clear();
            Item = item;
            Amount = 1;
        }

        public void Clear()
        {
            Item = null;
            Amount = 0;
        }

        public void AddItems(int amount)
        {
            Amount += amount;
        }
        public void RemoveItems(int amount) 
        {
            Amount -= amount;
            if (Amount == 0)
            {
                Clear();
            }
            else if (Amount < 0) { Debug.LogError("Try to remove more items than it exists in inventory"); Clear(); }
        }

    }

}
