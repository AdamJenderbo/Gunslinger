using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour
{
    Image slotSprite, itemIcon;
    Text itemAmount;

    private void Awake()
    {
        Image[] images = GetComponentsInChildren<Image>(true);
        slotSprite = images[0];
        itemIcon = images[1];
        itemAmount = itemIcon.GetComponentInChildren<Text>();
    }

    public void SetItemIcon(Sprite sprite)
    {
        itemIcon.sprite = sprite;
    }

    public void SetItemAmount(int amount)
    {
        if (amount < 2)
            itemAmount.text = "";
        else
            itemAmount.text = amount.ToString();
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
