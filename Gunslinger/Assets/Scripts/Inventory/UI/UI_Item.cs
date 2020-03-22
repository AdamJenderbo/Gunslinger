using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Item : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //public CanvasGroup canvasGroup;
    private Image image;
    private Item item;
    private Text amount;
    private CanvasGroup canvasGroup;

    private int itemAmount;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        image = transform.Find("image").GetComponent<Image>();
        amount = transform.Find("amount").GetComponent<Text>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .5f;
        canvasGroup.blocksRaycasts = false;
        Debug.Log("Begin drag");
    }

    public void OnDrag(PointerEventData evenData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        UI_DraggedItem.Instance.Hide();
        Debug.Log("End drag");
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetAmount(int amount)
    {
        itemAmount = amount;

        if(amount == 1)
            this.amount.text = "";
        else
            this.amount.text = amount.ToString();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void SetItem(Item item)
    {
        this.item = item;
        SetSprite(item.Sprite);
    }


}
