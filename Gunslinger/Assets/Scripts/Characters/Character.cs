using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public List<Sprite> sprites;

    [Header("Movement")]

    // movement

    public float moveSpeed;
    protected Rigidbody2D rigidBody;
    protected Vector2 moveDirection;


    public Bullet bullet;
    protected Gun gun;
    protected Vector3 target;
    private Transform aim;

    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    // private variables


    protected Camera cam;


    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //animator = GetComponent<Animator>();
        aim = transform.Find("Aim");
        gun = aim.Find("Right Hand").GetComponentInChildren<Gun>();
        gun.SetBullet(bullet);
        cam = Camera.main;
    }

    protected void LookAtTarget()
    {
        // rotate gun hand
        Vector2 offset = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;


  

        if (angle > 90 || angle < -90) { spriteRenderer.transform.localScale = Vector3.one; }
        else { spriteRenderer.transform.localScale = new Vector3(-1, 1, 1); }

        aim.rotation = Quaternion.Euler(0, 0, angle);

        // turn character



        // flip gun
        Vector3 localScale = Vector3.one;
        localScale.y = (angle > 90 || angle < -90) ? -1f : 1f;
        aim.localScale = localScale;
    }

    protected void SetTarget(Vector3 target)
    {
        this.target = target;
    }


    protected void FollowTarget()
    {
        moveDirection = target - transform.position;
        moveDirection.Normalize();
        Move();
    }

    protected void Move()
    {
        rigidBody.velocity = moveDirection * moveSpeed;
    }

    protected void Stop()
    {
        rigidBody.velocity = Vector2.zero;
    }
}
