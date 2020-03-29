using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
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


    #region Getters & Setters

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
        for (int i = 0; i < hotkeySlots.Length; i++)
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

    #endregion

    #region Public methods
    public bool AddItem(Item item)
    {
        Slot slot;
        if (item.Stackable)
        {
            slot = FindSlotWithItem(item);

            if (slot != null)
            {
                slot.AddItems(1);
                return true;
            }
        }

        if(Full)
            return false;

        slot = FindEmptySlot();
        if (slot == null)
        {
            Debug.LogError("Inventory.Full does not work!");
            return false;
        }

        slot.SetItem(item);
        return true;
    }

    public void AddItem(Item item, Slot Slot)
    {
        Slot.Clear();
        Slot.SetItem(item);
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

    #endregion

    #region Private methods

    private Slot FindSlotWithItem(Item item)
    {
        foreach(Slot slot in hotkeySlots)
        {
            if (slot.Item == item)
                return slot;
        }

        foreach(Slot slot in Slots)
        {
            if (slot.Item == item)
                return slot;
        }
        return null;
    }

    private Slot FindEmptySlot()
    {

        foreach (Slot slot in hotkeySlots)
        {
            if (slot.Empty)
                return slot;
        }

        foreach (Slot slot in Slots)
        {
            if (slot.Empty)
                return slot;
        }
        return null;
    }



    #endregion

    [Serializable]
    public class Slot
    {

        public delegate void OnItemChanged();
        public OnItemChanged onItemChangedCallback;

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
            UpdateUI();
        }

        public void Clear()
        {
            Item = null;
            Amount = 0;
            UpdateUI();
        }

        public void AddItems(int amount)
        {
            Amount += amount;
            UpdateUI();
        }
        public void RemoveItems(int amount) 
        {
            Amount -= amount;
            if (Amount == 0)
            {
                Clear();
            }
            else if (Amount < 0) { Debug.LogError("Try to remove more items than it exists in inventory"); Clear(); }
            UpdateUI();
        }

        public void SwitchSlots(Slot slot)
        {
            Item tmpItem = Item;
            int tmpAmount = Amount;
            Item = slot.Item;
            Amount = slot.Amount;
            slot.Item = tmpItem;
            slot.Amount = tmpAmount;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

    }

}
