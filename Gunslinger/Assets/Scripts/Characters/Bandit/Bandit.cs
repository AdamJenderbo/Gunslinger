using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Gunman
{
    public enum Behavior
    {
        Static, Chasing
    }
    // public variables

    public int health = 5;
    public int bounty;

    [Header ("Movement")]

    public float moveSpeed;
    public Rigidbody2D body;
    public Behavior behavior;
    public float range;

    [Header("Shooting")]

    public float shootRange;

    [Header("Animation")]

    public SpriteRenderer spriteRenderer;
    public Sprite banditBound;
    public Animator animator;

    public Action action;

    // private variables

    private enum State
    {
        FREE, BOUND, CAPTURED
    }

    private Vector3 moveDirection;
    private State state;
    private bool collidingWithPlayer;
    private bool grabbed;
    private Vector3 playerPosition;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = Player.instance.transform.position;

        switch (state)
        {
            case State.FREE:
                FreeState();
                break;
            case State.BOUND:
                BoundState();
                break;
            case State.CAPTURED:
                body.velocity = Vector2.zero;
                break;
        }
    }


    public void Capture()
    {
        state = State.CAPTURED;
        grabbed = false;
    }


    public void Damage(int damage)
    {
        if (state == State.BOUND)
            return;

        health -= damage;
        if (health <= 0)
        {
            Kill();
        }
    }

    // states

    private void FreeState()
    {
        bool playerWithinRange = Vector3.Distance(transform.position, Player.instance.transform.position) < range;

        if (playerWithinRange)
        {
            moveDirection = Player.instance.transform.position - transform.position;

            moveDirection.Normalize();

            if (behavior == Behavior.Chasing)
            {

                body.velocity = moveDirection * moveSpeed;
            }

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

            // shooting

            if (Vector3.Distance(transform.position, Player.instance.transform.position) < shootRange)
            {
                if (gun.Loaded)
                {
                    gun.Fire();
                }
            }



            if (behavior == Behavior.Chasing)
                animator.SetBool("Walking", true);
        }
        else
        {
            body.velocity = Vector2.zero;

            if (behavior == Behavior.Chasing)
                animator.SetBool("Walking", false);

        }
    }

    private void BoundState()
    {
        if (collidingWithPlayer)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                grabbed = !grabbed;
            }
        }

        if(grabbed)
        {
            if(Player.instance.direction == Player.Direction.LEFT)
            {
                transform.position = new Vector3(playerPosition.x + 0.3f, playerPosition.y - 0.2f, playerPosition.z);
            }
            else if(Player.instance.direction == Player.Direction.RIGHT)
            {
                transform.position = new Vector3(playerPosition.x - 0.3f, playerPosition.y - 0.2f, playerPosition.z);
            }
        }
        else
            body.velocity = Vector2.zero;
    }


    private void Kill()
    {
        state = State.BOUND;
        gun.Drop();
        body.velocity = Vector2.zero;
        animator.SetBool("Walking", false);
        spriteRenderer.sprite = banditBound;
        frontHand.SetActive(false);
        backHand.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            collidingWithPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            collidingWithPlayer = false;
        }
    }



}
