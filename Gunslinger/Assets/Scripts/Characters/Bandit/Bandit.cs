using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Gunman
{
    // public variables

    public int health = 5;
    public int bounty;

    [Header ("Movement")]

    public float moveSpeed;
    public Rigidbody2D body;


    [Header("Shooting")]

    public float detectRange;
    public float shootRange;
    public float chaseRange;

   [Header("Animation")]

    public SpriteRenderer spriteRenderer;
    public Sprite banditBound;
    public Animator animator;

    public Action action;

    // private variables

    private Vector3 moveDirection;
    private bool collidingWithPlayer;
    private bool grabbed;
    private Vector3 playerPosition;

    private State state;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ChangeState(new Static(this));
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = Player.instance.transform.position;
        state.Update();
    }

    private void ChangeState(State state)
    {
        this.state = state;
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Kill();
        }
    }


    private void Kill()
    {
        gun.Drop();
        body.velocity = Vector2.zero;
        animator.SetBool("Walking", false);
        spriteRenderer.sprite = banditBound;
        frontHand.SetActive(false);
        backHand.SetActive(false);
    }

    private void TargetPlayer()
    {
        // flip sprite

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

    private abstract class State
    {
        protected Bandit bandit;

        protected bool playerDetected { get { return Vector3.Distance(bandit.transform.position, Player.instance.transform.position) < bandit.detectRange; } }
        protected bool playerWithinShootRange { get { return Vector3.Distance(bandit.transform.position, Player.instance.transform.position) < bandit.shootRange; } }
        protected bool playerWithinChaseRange { get { return Vector3.Distance(bandit.transform.position, Player.instance.transform.position) < bandit.chaseRange; } }

        public State(Bandit bandit)
        {
            this.bandit = bandit;
        }
        
        public abstract void Update();
    }


    private class Static : State
    {
        public Static(Bandit bandit) : base(bandit)
        {
            bandit.body.velocity = Vector2.zero;
            bandit.animator.SetBool("Walking", false);
            Debug.Log("Static");
        }

        public override void Update()
        {
            if (playerDetected)
            {
                bandit.ChangeState(new Chasing(bandit));
            }
        }
    }

    private class Patrolling : State
    {
        public Patrolling(Bandit bandit) : base(bandit)
        {
            bandit.animator.SetBool("Walking", true);
            Debug.Log("Patrolling");
        }

        public override void Update()
        {
            // patrol behavior

            if (playerDetected)
            {
                bandit.ChangeState(new Chasing(bandit));
            }
        }
    }


    private class Searching : State
    {
        public Searching(Bandit bandit) : base(bandit)
        {
        }

        public override void Update()
        {
        }
    }

    private class Chasing : State
    {
        public Chasing(Bandit bandit) : base(bandit)
        {
            bandit.animator.SetBool("Walking", true);
            Debug.Log("Chasing");
        }

        public override void Update()
        {
            bandit.moveDirection = Player.instance.transform.position - bandit.transform.position;
            bandit.moveDirection.Normalize();
            bandit.body.velocity = bandit.moveDirection * bandit.moveSpeed;

            bandit.transform.localScale = new Vector3(-1f, 1f, 1f);

            bandit.TargetPlayer();

            if (playerWithinShootRange) // starts shooting player
            {
                bandit.ChangeState(new Shooting(bandit));
            }
            else if(!playerWithinChaseRange) // stops chasing player
            {
                bandit.ChangeState(new Static(bandit));
            }
        }
    }


    private class Shooting : State
    {
        public Shooting(Bandit bandit) : base(bandit)
        {
            bandit.body.velocity = Vector2.zero;
            bandit.animator.SetBool("Walking", false);
            Debug.Log("Shooting");
        }

        public override void Update()
        {
            bandit.TargetPlayer();

            if (bandit.gun.Loaded)
            {
                bandit.gun.Fire();
            }

            if(!playerWithinShootRange)
            {
                if(playerWithinChaseRange)
                {
                    bandit.ChangeState(new Chasing(bandit));
                }
                else
                {
                    bandit.ChangeState(new Static(bandit));
                }
            }
        }
    }

}
