using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Weapon : MonoBehaviour
{

    public static UI_Weapon Instance;

    public Image icon1;
    public Image icon2;


    private void Awake()
    {
        Instance = this;
    }

    public void SetIcon1(Sprite sprite)
    {
        icon1.sprite = sprite;
    }

    public void SetIcon2(Sprite sprite)
    {
        icon2.sprite = sprite;
    }
}
