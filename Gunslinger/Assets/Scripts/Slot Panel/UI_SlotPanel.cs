using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SlotPanel : MonoBehaviour
{

    public int rows, colums;
    public UI_Slot[,] ui_slots;

    public float slotSize;

    public Transform slotPrefab;
    public Transform slotContainer;

    protected List<Inventory.Slot> slots;

    public List<Item> testItems;

    // Start is called before the first frame update
    void Start()
    {
        slots = new List<Inventory.Slot>();
        for(int i = 0; i < testItems.Count; i++)
        {
            slots.Add(new Inventory.Slot());
            slots[i].Item = testItems[i];
            slots[i].Amount = 1;
        }
        CreatePanel();
        DrawIcons();
    }

    private void CreatePanel()
    {
        slotContainer.GetComponent<RectTransform>().anchoredPosition += new Vector2((rows / 2) * -slotSize, slotSize); // move slot container to middle
        GetComponent<RectTransform>().sizeDelta = new Vector2(350, 350); // scale panel

        ui_slots = new UI_Slot[rows, colums];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < colums; col++)
            {
                ui_slots[row, col] = Instantiate(slotPrefab, slotContainer).GetComponent<UI_Slot>();
                ui_slots[row, col].GetComponent<RectTransform>().anchoredPosition = new Vector2(col * slotSize, -row * slotSize);
                ui_slots[row, col].gameObject.SetActive(true);
            }
        }
    }

    private void DrawIcons()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < colums; col++)
            {
                if (col + (rows * row) >= slots.Count)
                    return;

                Inventory.Slot slot = slots[col + (rows * row)];

                if (!slot.Empty)
                {
                    ui_slots[row, col].SetItemIcon(slot.Item.Sprite);
                    ui_slots[row, col].ShowIcon();
                }
                else
                {
                    ui_slots[row, col].HideIcon();
                }
            }
        }
    }
}
