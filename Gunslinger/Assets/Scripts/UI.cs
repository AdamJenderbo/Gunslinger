using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance;

    // health bar

    public Slider healthSilder;
    public Text healthText;

    // money counter

    public Text moneyText;

    // game Over

    public GameObject gameOverScreen;

    // popup
    public GameObject popup;
    public Text popupText;

    private bool popupShowing;

    // inventory

    public UI_Inventory inventory;

    // shop

    public UI_Shop ui_shop;

    // loot

    public UI_Loot ui_loot;


    public int maxHP
    {
        set { healthSilder.maxValue = value; }
    }

    public int HP
    {
        set { healthSilder.value = value; }
    }

    public int Money
    {
        set { moneyText.text = value.ToString(); }
    }

    private void Awake()
    {
        instance = this;
        inventory.Hide();
    }

    // Start is called before the first frame update
    void Start()
    {
        popupShowing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(popupShowing && Input.GetKeyDown(KeyCode.Return))
        {
            HidePopup();
        }

        // show and hide inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.hidden)
                inventory.Show();
            else
                inventory.Hide();
        }
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void ShowPopup(string text)
    {
        popupShowing = true;
        popup.SetActive(true);
        popupText.text = text;
    }

    public void HidePopup()
    {
        popupShowing = false;
        popup.SetActive(false);
    }

    public void ShowShop(Shop shop)
    {
        ui_shop.gameObject.SetActive(true);
        ui_shop.DrawShop(shop);
    }

    public void HideShop()
    {
        ui_shop.gameObject.SetActive(false);
    }

    public void ShowLoot(Loot loot)
    {
        ui_loot.gameObject.SetActive(true);
        ui_loot.Draw(loot);
    }

    public void HideLoot()
    {
        ui_loot.gameObject.SetActive(false);
    }
}
