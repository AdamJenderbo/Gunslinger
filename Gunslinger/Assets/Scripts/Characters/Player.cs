using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : Character
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

    public GunItem equippedGun;
    public GunItem secondGun;


    int ammo;

    // modules

    public IMoveVelocity moveVelocity;
    public ICharacterAim aim;
    [HideInInspector]
    public Shooting shooting;


    private void Awake()
    {
        instance = this;
        inventory = new Inventory(13);
    }

    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
        moveVelocity = GetComponent<IMoveVelocity>();
        aim = GetComponent<ICharacterAim>();
        shooting = GetComponent<Shooting>();

        dead = false;
        ammo = 50;

        PlayIdleAnimation();

        UI.instance.maxHP = maxHp;
        UI.instance.HP = hp;
        UI.instance.Ammo = ammo;

        UI_Weapon.Instance.SetIcon1(equippedGun.Sprite);
        UI_Weapon.Instance.SetIcon2(secondGun.Sprite);
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwitchGun();
        }

        Vector3 velocity = moveVelocity.GetVelocity();

        if (velocity == Vector3.zero)
            PlayIdleAnimation();
        else
            PlayWalkAnimation();
    }


    public void Damage(int damage)
    {
        hp -= damage;
        UI.instance.HP = hp;
        Flash();
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

    public void PickUpGun(GunItem gun)
    {
        shooting.DropGun();
        shooting.SetGun(gun.gun);
        equippedGun = gun;

        UI_Weapon.Instance.SetIcon1(equippedGun.Sprite);
        UI_Weapon.Instance.SetIcon2(secondGun.Sprite);
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

    public void SwitchGun()
    {
        GunItem tmp = equippedGun;
        equippedGun = secondGun;
        secondGun = tmp;
        shooting.SetGun(equippedGun.gun);

        UI_Weapon.Instance.SetIcon1(equippedGun.Sprite);
        UI_Weapon.Instance.SetIcon2(secondGun.Sprite);
    }
}
