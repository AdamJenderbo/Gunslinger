using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
    public Transform shopItemPrefab;
    public Transform shopItemContainer;

    float shopItemSize = 120;
    float offsetY = 230;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawShop(Shop shop)
    {
        foreach (Transform child in shopItemContainer)
        {
            if (child == shopItemPrefab) continue;
            Destroy(child.gameObject);
        }

        List<Shop.ShopItem> shopItems = shop.GetShopItems();
        for(int i = 0; i < shopItems.Count; i++)
        {
            Shop.ShopItem shopItem = shopItems[i];

            RectTransform shopItemRectTransform = Instantiate(shopItemPrefab, shopItemContainer).GetComponent<RectTransform>();
            shopItemRectTransform.anchoredPosition = new Vector2(0, -i * shopItemSize + offsetY);
            shopItemRectTransform.gameObject.SetActive(true);

            UI_ShopItem ui_shopItem = shopItemRectTransform.GetComponent<UI_ShopItem>();

            ui_shopItem.SetSprite(shopItem.Item.Sprite);
            ui_shopItem.SetCost(shopItem.Cost);

            UI_Button button = shopItemRectTransform.GetComponent<UI_Button>();
            button.MouseLeftClickFunc = () =>
            {
                shop.ItemLeftClicked(shopItem);
            };

        }
    }
}
