using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum Hand { singleHand, doubleHand }

    public Hand hand;

    public Transform firePoint;
    public GameObject bulletToFire;
    public float fireDelay;
    private Cooldown shotCooldown;

    public GunPickup pickup;
    public Gun gunPrefab;

    public Item.ItemType gunType;

    public bool Loaded { get { return shotCooldown.Done(); } }

    // Start is called before the first frame update
    void Start()
    {
        shotCooldown = new Cooldown(fireDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Loaded)
            shotCooldown.Update();
    }

    public void Fire()
    {
        if(Loaded)
        {
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            shotCooldown.Reset();
        }       
    }

    public void Drop()
    {
        Instantiate(pickup, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
