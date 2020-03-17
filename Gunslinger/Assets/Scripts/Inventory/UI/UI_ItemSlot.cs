using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour, IDropHandler
{
    private Action onDropAction;

    public void SetOnDropAction(Action onDropAction)
    {
        this.onDropAction = onDropAction;
    }

    public void OnDrop(PointerEventData eventData)
    {
        UI_DraggedItem.Instance.Hide();
        onDropAction();
    }
}
