using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SlotPanel : MonoBehaviour
{

    public int rows, colums;
    public UI_Slot[,] ui_slots;

    public float slotSize;

    public Transform slotPrefab;
    public Transform slotContainer;

    // Start is called before the first frame update
    void Start()
    {

        slotContainer.GetComponent<RectTransform>().anchoredPosition += new Vector2((rows / 2) * -slotSize, slotSize); // move slot container to middle
        GetComponent<RectTransform>().sizeDelta = new Vector2(350, 350); // scale panel

        ui_slots = new UI_Slot[rows, colums];

        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < colums; j++)
            {
                ui_slots[i,j] = Instantiate(slotPrefab, slotContainer).GetComponent<UI_Slot>();
                ui_slots[i, j].GetComponent<RectTransform>().anchoredPosition = new Vector2(i * slotSize, -j * slotSize);
                ui_slots[i, j].gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
