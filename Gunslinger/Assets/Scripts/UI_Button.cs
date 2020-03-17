using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{


    public Action MouseLeftClickFunc = null;
    public Action MouseRightClickFunc = null;
    public Action MouseMiddleClickFunc = null;
    public Action MouseEnterFunc = null;
    public Action MouseExitFunc = null;
    public Action MouseDownFunc = null;
    public Action MouseUpFunc = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (MouseLeftClickFunc != null) MouseLeftClickFunc();
        }
        
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (MouseRightClickFunc != null) MouseRightClickFunc();
        }

        if (eventData.button == PointerEventData.InputButton.Middle)
        {
            if (MouseMiddleClickFunc != null) MouseMiddleClickFunc();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (MouseDownFunc != null) MouseDownFunc();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MouseEnterFunc != null) MouseEnterFunc();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MouseExitFunc != null) MouseExitFunc();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (MouseUpFunc != null) MouseUpFunc();
    }
}
