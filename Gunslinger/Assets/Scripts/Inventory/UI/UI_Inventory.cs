using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : UI_SlotPanel
{
    //public Transform pfUI_Item;

    Inventory inventory;

    //public Transform itemSlotContainer;
    //public Transform itemSlotTemplate;

    public Transform hotkeySlotContainer;

    public UI_ItemSlot gunSlot;
    public Image gunImage;

    public bool hidden;
    //float itemSlotCellSize = 96f;
    private int nrHotkeySlots = 4;

    public List<Sprite> slotSprites;

    private void Awake()
    {
        //itemSlotContainer = transform.Find("Item Slots");
        //itemSlotTemplate = itemSlotContainer.Find("Item Slot");
        //itemSlotTemplate.gameObject.SetActive(false);
        rows = 3;
        colums = 3;
        Setup();
    }

    public void Start()
    {
        inventory = Player.instance.GetInventory();
        Inventory.Slot[] inventorySlots = inventory.GetSlots();
        List<Inventory.Slot> slots = new List<Inventory.Slot>();
        for(int i = 0; i < inventorySlots.Length - 4; i++)
        {
            slots.Add(inventorySlots[i]);
        }
        SetSlots(slots);
        if (inventory == null) Debug.LogError("null");
        inventory.onItemChangedCallback += RefreshInventory;
        DrawPanel();
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        //RefreshHotKeySlots();

        if (hidden)
            return;

        DrawIcons();
        //CreateItemSlots(nrHotkeySlots, inventory.Space - nrHotkeySlots, colums, itemSlotContainer);
    }

    public void RefreshGunSlot()
    {
        gunImage.sprite = inventory.GetGun().Sprite;
    }

    public void RefreshHotKeySlots()
    {
        CreateItemSlots(0, nrHotkeySlots, nrHotkeySlots, hotkeySlotContainer);
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

    private void CreateItemSlots(int start, int nr, int colums, Transform container)
    {
        /*
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        for (int i = start; i < start + nr; i++)
        {
            Inventory.Slot inventorySlot = inventory.GetSlots()[i];
            Item item = inventorySlot.Item;
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, container).GetComponent<RectTransform>();
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);
            Image slotImage =  itemSlotRectTransform.GetComponent<Image>();
            slotImage.sprite = slotSprites[i];
            itemSlotRectTransform.gameObject.SetActive(true);

            if (!inventorySlot.Empty)
            {
                Transform uiItemTransform = Instantiate(pfUI_Item, container);
                uiItemTransform.GetComponent<RectTransform>().anchoredPosition = itemSlotRectTransform.anchoredPosition;
                UI_Item uiItem = uiItemTransform.GetComponent<UI_Item>();
                uiItem.SetItem(item);
                uiItem.SetSprite(item.Sprite);
                uiItem.SetAmount(inventorySlot.Amount);
                UI_Button button = uiItem.GetComponent<UI_Button>();
                button.MouseRightClickFunc = () =>
                {
                    inventory.UseItem(item);
                };
            }

            Inventory.Slot tmpInventorySlot = inventorySlot;
            UI_ItemSlot uiItemSlot = itemSlotRectTransform.GetComponent<UI_ItemSlot>();
            uiItemSlot.SetOnDropAction(() =>
            {
                Item draggedItem = UI_DraggedItem.Instance.GetItem();
                inventory.MoveItem(draggedItem, tmpInventorySlot);
            });

            x++;
            if (x == colums)
            {
                x = 0;
                y++;
            }
            
        }
    */
    }
}
