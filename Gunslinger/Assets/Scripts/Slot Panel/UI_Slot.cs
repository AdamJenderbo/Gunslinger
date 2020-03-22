using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Slot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    Image slotSprite, itemIcon;
    Inventory.Slot slot;
    Text itemAmount;
    Item item;

    Action onDropAction;

    private void Awake()
    {
        Image[] images = GetComponentsInChildren<Image>(true);
        slotSprite = images[0];
        itemIcon = images[1];
        itemAmount = itemIcon.GetComponentInChildren<Text>();
        onDropAction = () =>
        {
            Inventory.Slot draggedSlot = UI_DraggedItem.Instance.GetSlot();
            Inventory.Slot.SwitchSlots(draggedSlot, slot);
        };
    }

    public void SetOnDropAction(Action onDropAction)
    {
        this.onDropAction = onDropAction;
    }

    public void SetSlot(Inventory.Slot slot)
    {
        this.slot = slot;
    }

    public void ShowIcon()
    {
        itemIcon.gameObject.SetActive(true);
        itemIcon.sprite = slot.Item.Sprite;
        if (slot.Amount < 2)
            itemAmount.text = "";
        else
            itemAmount.text = slot.Amount.ToString();
    }

    public void HideIcon()
    {
        itemIcon.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin drag");
        itemIcon.color = new Color(itemIcon.color.r, itemIcon.color.g, itemIcon.color.b, 0.5f);
        UI_DraggedItem.Instance.Show(slot);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag");
        itemIcon.color = new Color(itemIcon.color.r, itemIcon.color.g, itemIcon.color.b, 1f);
        UI_DraggedItem.Instance.Hide();
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        UI_DraggedItem.Instance.Hide();
        onDropAction();
    }
}
