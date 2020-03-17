using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DraggedItem : MonoBehaviour
{
    public static UI_DraggedItem Instance { get; private set; }

    private RectTransform parentRectTransform;
    private Item item;
    private Image image;
    private Text amount;

    private void Awake()
    {
        Instance = this;

        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        image = transform.Find("image").GetComponent<Image>();
        amount = transform.Find("amount").GetComponent<Text>();

        Hide();
    }

    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, Input.mousePosition, null, out Vector2 localPoint);
        transform.localPosition = localPoint;
    }

    public Item GetItem()
    {
        return item;
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetAmount(int amount)
    {
        if(amount == 1)
            this.amount.text = "";
        else
            this.amount.text = amount.ToString();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(Item item, int amount)
    {
        gameObject.SetActive(true);

        SetItem(item);
        SetSprite(item.Sprite);
        SetAmount(amount);
        UpdatePosition();
    }
}
