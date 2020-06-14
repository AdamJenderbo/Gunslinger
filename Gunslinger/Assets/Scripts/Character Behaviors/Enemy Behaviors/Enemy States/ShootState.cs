using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : State
{
    State search;
    float attackCounter;

    protected override void Awake()
    {
        base.Awake();
        search = GetComponent<SearchState>();
    }

    public override void Begin()
    {
        enemy.ToShootView();
        attackCounter = 0;
    }

    private void Update()
    {
        attackCounter -= Time.deltaTime;
        enemy.Stop();
        enemy.AimAt(enemy.player.transform.position);

        if(attackCounter <= 0f)
        {
            enemy.shooting.Shoot();
            attackCounter = enemy.attackSpeed;
        }


        if (!enemy.SeesPlayer())
            enemy.ChangeState(search);

    }

    public override void End()
    {
    }
}
