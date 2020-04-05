using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : Character, ICustomer
{    
    public static Player instance; // singleton instance

    [Header("Stats")]
   
    public int hp;
    public int maxHp;

    [Header("Shooting")]

    // private variables

    private bool dead;

    #region Inventory

    Inventory inventory;

    Inventory.Slot weaponSlot;

    #endregion

    public Gun secondGun;
    int ammo;

    private void Awake()
    {
        instance = this;
        inventory = new Inventory(13);
        //inventory.SetGun(new GunItem(gun));
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        dead = false;
        ammo = 50;
        UI.instance.maxHP = maxHp;
        UI.instance.HP = hp;
        UI.instance.Ammo = ammo;
    }

    // Update is called once per frame
    private void Update()
    {
        if(!dead)
        {            
            // movement

            moveDirection.x = Input.GetAxisRaw("Horizontal");
            moveDirection.y = Input.GetAxisRaw("Vertical");
            moveDirection.Normalize();
            Move();

            Vector3 target = cam.ScreenToWorldPoint(Input.mousePosition);
            SetTarget(target);
            LookAtTarget();

            // shooting

            if (Input.GetMouseButtonDown(0))
            {
                if(ammo > 0)
                {
                    if (gun.Fire())
                    {
                        ammo--;
                        UI.instance.Ammo = ammo;
                    }
                }
         
            }

            // switch gun

            if(Input.GetKeyDown(KeyCode.Space))
            {
                SwitchGun(secondGun);
            }


            #region Hotkeys
            if (Input.GetKeyDown(KeyCode.Alpha1))
                inventory.UseHotKey(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                inventory.UseHotKey(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                inventory.UseHotKey(2);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                inventory.UseHotKey(3);
            #endregion


            // update animation

            //animator.SetBool("Walking", moveDirection != Vector2.zero);
        }
        else
        {
            Stop();
            //animator.SetBool("Walking", false);
        }

    }

    public void Damage(int damage)
    {
        hp -= damage;
        UI.instance.HP = hp;
        if (hp <= 0)
        {
            dead = true;
            //UI.instance.ShowGameOverScreen();
        }

    }

    public void Heal(int amount)
    {
        hp += amount;
        UI.instance.HP = hp;
    }

    public bool PickUp(Item item)
    {
        return inventory.AddItem(item);
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public bool CanAffordItem(int cost)
    {
        return MoneyManager.instance.Amount >= cost;
    }

    public bool CanBuyItem(Item item)
    {
        return !inventory.Full;
    }
    public void BuyItem(Item item, int cost)
    {
        MoneyManager.instance.Decrease(cost);
        inventory.AddItem(item);
    }

    public void AddMoney(int amount)
    {
        MoneyManager.instance.Increase(amount);
    }

    public void AddAmmo(int amount)
    {
        ammo += amount;
        UI.instance.Ammo = ammo;
    }


}
