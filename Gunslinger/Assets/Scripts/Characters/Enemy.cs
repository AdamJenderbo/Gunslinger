using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public float detectRange, shootRange, chaseRange;
    float attackDelay, attackCounter;
    public int health;


    private bool playerDetected { get { return Vector3.Distance(transform.position, target) < detectRange; } }
    private bool playerWithinShootRange { get { return Vector3.Distance(transform.position, target) < shootRange; } }
    private bool playerWithinChaseRange { get { return Vector3.Distance(transform.position, target) < chaseRange; } }

    Pathfinding pathfinding;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        gun.Reload(gun.maxAmmo);
        attackDelay = 1f;
        attackCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCounter > 0) attackCounter -= Time.deltaTime;

        SetTarget(Player.instance.transform.position);
        LookAtTarget();

        if (playerWithinShootRange)
        {
            if (attackCounter <= 0)
            {
                if (!gun.Loaded)
                {
                    gun.Reload(gun.maxAmmo);
                }
                Stop();
                gun.Fire();
                attackCounter = attackDelay;
            }
        }
        else if(playerWithinChaseRange)
        {
            FollowTarget();
        }
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0) Kill();
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
