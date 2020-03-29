using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, ICustomer
{    
    public static Player instance; // singleton instance

    public enum Direction
    {
        LEFT, RIGHT
    }


    [Header("Stats")]
   
    public int hp;
    public int maxHp;

    [Header("Movement")]

    [HideInInspector]
    public Direction direction;

    [Header("Shooting")]

    public Gun gun;
    public Gun newGun;
    public Transform frontHand, backHand;
    public Transform gunHand;

    [Header("Graphics")]

    public SpriteRenderer sprite;
    public Animator animator;


    // private variables

    private bool dead;
    private Camera cam;

    private Transform grabbedBandit;
    private float banditOffsetX, banditOffsetY;

    #region Inventory

    Inventory inventory;

    Inventory.Slot weaponSlot;

    #endregion

    private void Awake()
    {
        instance = this;
        inventory = new Inventory(13);
        //inventory.SetGun(new GunItem(gun));
    }

    // Start is called before the first frame update
    void Start()
    {
        dead = false;

        //UI.instance.maxHP = maxHp;
        //UI.instance.HP = hp;

        cam = Camera.main;

        banditOffsetX = -0.3f;
        banditOffsetY = -0.2f;

        grabbedBandit = null;

    }

    // Update is called once per frame
    void Update()
    {
        if(!dead)
        {
            
            // movement

            moveDirection.x = Input.GetAxisRaw("Horizontal");
            moveDirection.y = Input.GetAxisRaw("Vertical");
            moveDirection.Normalize();
            Move();

            // mouse postion

            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = cam.WorldToScreenPoint(transform.localPosition);


            transform.localScale = new Vector3(-1f, 1f, 1f);

            // turn player

            if (mousePos.x > screenPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunHand.localScale = Vector3.one;
                direction = Direction.RIGHT;
            }
            else
            {
                transform.localScale = Vector3.one;
                gunHand.localScale = new Vector3(-1f, -1f, 1f);
                direction = Direction.LEFT;
            }

            // rotate gun hand

            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            gunHand.rotation = Quaternion.Euler(0, 0, angle);

            // shooting

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                gun.Fire();

            // use hotkey

            if (Input.GetKeyDown(KeyCode.Alpha1))
                inventory.UseHotKey(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                inventory.UseHotKey(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                inventory.UseHotKey(2);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                inventory.UseHotKey(3);



            if (grabbedBandit != null)
                grabbedBandit.position = new Vector3(transform.position.x + banditOffsetX, transform.position.y + banditOffsetY, transform.position.z);

            // update animation

            animator.SetBool("Walking", moveDirection != Vector2.zero);
        }
        else
        {
            Stop();
            animator.SetBool("Walking", false);
        }

    }

    public void Damage(int damage)
    {
        hp -= damage;
        UI.instance.HP = hp;
        if (hp <= 0)
        {
            dead = true;
            UI.instance.ShowGameOverScreen();
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

    public void SwitchGun(Gun newGun)
    {
        if (newGun == null)
            return;

        //inventory.AddItem(new GunItem(GunPrefabs.Instance.GetGunPrefab(gun.gunType)));

        Destroy(gun.gameObject);

        //inventory.SetGun(new GunItem(newGun));

        if (newGun.hand == Gun.Hand.singleHand)
        {
            gunHand = frontHand.transform;
            frontHand.gameObject.SetActive(true);
        }
        else if (newGun.hand == Gun.Hand.doubleHand)
        {
            gunHand = backHand.transform;
            frontHand.gameObject.SetActive(false);
        }


        gun = Instantiate(newGun);

        gun.transform.parent = gunHand.transform;
        gun.transform.position = gunHand.position;
        gun.transform.localRotation = Quaternion.Euler(Vector3.zero);
        gun.transform.localScale = Vector3.one;
    }

    public void GrabBandit(Transform banditPosition)
    {
        grabbedBandit = banditPosition;
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
}
