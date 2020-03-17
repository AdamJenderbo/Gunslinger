using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<ShopItem> shopItems;
    ICustomer customer;

    bool open;

    // Start is called before the first frame update
    void Start()
    {
        Trigger trigger = GetComponent<Trigger>();
        trigger.onTriggerAction = (Player player) =>
        {
            if(!open)
            {
                open = true;
                ICustomer customer = player.GetComponent<ICustomer>();
                if (customer == null)
                    return;
                SetCustomer(customer);
                ShowUI();
            }
            else
            {
                open = false;
                HideUI();
            }
        };
    }

    public List<ShopItem> GetShopItems()
    {
        return shopItems;
    }

    public void SetCustomer(ICustomer customer)
    {
        this.customer = customer;
    }

    public void ItemLeftClicked(ShopItem shopItem)
    {
        BuyItem(shopItem);
    }

    private void BuyItem(ShopItem shopItem)
    {
        Debug.Log("buy");

        if(!customer.CanAffordItem(shopItem.Cost))
        {
            Debug.Log("Customer can't afford item");
            return;
        }
        if(!customer.CanBuyItem(shopItem.Item))
        {
            Debug.Log("Customer can't buy item");
        }

        customer.BuyItem(shopItem.Item, shopItem.Cost);
    }


    private void ShowUI()
    {
        UI.instance.ShowShop(this);
    }

    private void HideUI()
    {
        UI.instance.HideShop();
    }


    [System.Serializable]
    public class ShopItem
    {
        [SerializeField]
        public Item Item;
        public int Cost; 

        public ShopItem(Item item, int cost)
        {
            Item = item;
            Cost = cost;
        }
    }
}
