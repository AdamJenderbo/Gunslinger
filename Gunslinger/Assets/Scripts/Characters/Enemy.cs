using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D body;
    public float moveSpeed;

    public int health = 5;

    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    [Header("Shooting")]

    public Transform gunHand;
    public Transform firePoint;
    public GameObject bulletToFire;
    public float fireDelay;
    private Cooldown shotCooldown;
    public float shootRange;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        shotCooldown = new Cooldown(fireDelay);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = Player.instance.transform.position;

        // if player within range
        if(Vector3.Distance(transform.position, Player.instance.transform.position) < rangeToChasePlayer)
        {
            moveDirection = Player.instance.transform.position - transform.position;
        }

        moveDirection.Normalize();
        body.velocity = moveDirection * moveSpeed;

        transform.localScale = new Vector3(-1f, 1f, 1f);

        
        if (moveDirection.x > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);

            gunHand.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = Vector3.one;
            gunHand.localScale = new Vector3(-1f, -1f, 1f);
        }
        
        // rotate gun hand
        
        Vector2 offset = new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunHand.rotation = Quaternion.Euler(0, 0, angle);
        


        if (Vector3.Distance(transform.position, Player.instance.transform.position) < shootRange)
        {
            shotCooldown.Update();
            if (shotCooldown.Done())
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                shotCooldown.Reset();
            }
        }


        animator.SetBool("Walking", moveDirection != Vector3.zero);
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
