using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour
{
    Image itemIcon;
    private void Start()
    {
        itemIcon = GetComponentInChildren<Image>();
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
