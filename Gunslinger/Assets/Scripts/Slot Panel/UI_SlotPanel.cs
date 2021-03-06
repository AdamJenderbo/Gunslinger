﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SlotPanel : MonoBehaviour
{
    protected int rows, colums;
    public UI_Slot[,] ui_slots;
    public UI_Button[,] ui_buttons;

    float slotSize;

    public Transform slotPrefab;
    public Transform slotContainer;
    RectTransform header;
    Text headerText;
    float border;

    protected List<Inventory.Slot> slots;

    protected void Setup()
    {
        slotSize = 95;
        border = 35;
        slots = new List<Inventory.Slot>();
        header = GetComponentsInChildren<Image>()[1].rectTransform;
        headerText = header.GetComponentInChildren<Text>();
    }

    protected void SetSlots(List<Inventory.Slot> slots)
    {
        this.slots = slots;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < colums; col++)
            {
                ui_slots[row, col].SetSlot(slots[col + (colums * row)]);
            }
        }
    }

    protected void SetHeaderText(string text)
    {
        headerText.text = text;
    }

    protected void DrawPanel()
    {
        foreach (Transform child in slotContainer)
        {
            if (child == slotPrefab) continue;
            Destroy(child.gameObject);
        }
        slotContainer.GetComponent<RectTransform>().anchoredPosition += new Vector2(-(slotSize / 2) * (colums - 1), (slotSize / 2) * (rows - 1)); // move container to center
        GetComponent<RectTransform>().sizeDelta = new Vector2((100 * colums) + 50, (100 * rows) + 50); // scale panel
        Debug.Log("Slot size: " + slotSize);
        Debug.Log("Rows: " + rows + " Colums: " + colums);

        ui_slots = new UI_Slot[rows, colums];
        ui_buttons = new UI_Button[rows, colums];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < colums; col++)
            {
                ui_slots[row, col] = Instantiate(slotPrefab, slotContainer).GetComponent<UI_Slot>();
                ui_slots[row, col].GetComponent<RectTransform>().anchoredPosition = new Vector2(col * slotSize, -row * slotSize);
                ui_slots[row, col].gameObject.SetActive(true);
                ui_buttons[row, col] = ui_slots[row, col].GetComponent<UI_Button>();
            }
        }

        header.anchoredPosition += new Vector2(-((slotSize / 2) * (colums - 2)+10), ((slotSize / 2) * rows) + border );
    }

    protected void DrawIcons()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < colums; col++)
            {
                if (col + (rows * row) >= slots.Count)
                    return;

                Inventory.Slot slot = slots[col + (colums * row)];

                if (!slot.Empty)
                {
                    ui_slots[row, col].ShowIcon();
                }
                else
                {
                    ui_slots[row, col].HideIcon();
                }
            }
        }
    }

    protected void SetButtonAction(int row, int col, Action action)
    {
        ui_buttons[row, col].MouseLeftClickFunc = action;
    }
}
