using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour
{
    Image slotSprite, itemIcon;

    private void Awake()
    {
        Image[] images = GetComponentsInChildren<Image>(true);
        slotSprite = images[0];
        itemIcon = images[1];
    }

    public void SetItemIcon(Sprite sprite)
    {
        itemIcon.sprite = sprite;
    }


    public void ShowIcon()
    {
        itemIcon.gameObject.SetActive(true);
    }

    public void HideIcon()
    {
        itemIcon.gameObject.SetActive(false);
    }

}
