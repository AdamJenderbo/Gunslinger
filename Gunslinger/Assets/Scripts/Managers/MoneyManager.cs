using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{

    public static MoneyManager instance;

    int money = 0;

    public int Amount { get { return money; } }


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UI.instance.Money = money;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Increase(int amount)
    {
        money += amount;
        UI.instance.Money = money;
    }

    public void Decrease(int amount)
    {
        money -= amount;
        UI.instance.Money = money;
    }
}
