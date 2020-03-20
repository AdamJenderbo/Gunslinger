using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SlotPanel : MonoBehaviour
{

    public int rows, colums;
    public UI_Slot[,] ui_slots;

    float slotSize;

    public Transform slotPrefab;
    public Transform slotContainer;

    protected List<Inventory.Slot> slots;

    // Start is called before the first frame update
    void Start()
    {
        slotSize = 95;
        slots = new List<Inventory.Slot>();
        DrawPanel();
        DrawIcons();
    }


    protected void SetSlots(List<Inventory.Slot> slots)
    {
        this.slots = slots;
    }

    protected void DrawPanel()
    {
        slotContainer.GetComponent<RectTransform>().anchoredPosition += new Vector2(-(slotSize / 2) * (colums - 1), (slotSize / 2) * (rows - 1)); // move container to center
        GetComponent<RectTransform>().sizeDelta = new Vector2((100 * colums) + 50, (100 * rows) + 50); // scale panel

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

    protected void DrawIcons()
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
