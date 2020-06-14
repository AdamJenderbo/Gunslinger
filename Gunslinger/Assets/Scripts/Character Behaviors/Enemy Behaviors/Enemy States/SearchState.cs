using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : State
{
    State idle;
    State shoot;

    Vector3 playerLastSeen;

    protected override void Awake()
    {
        base.Awake();
        idle = GetComponent<IdleState>();
        shoot = GetComponent<ShootState>();
    }

    public override void Begin()
    {

        enemy.ToSearchView();
        playerLastSeen = enemy.player.transform.position;
        enemy.MoveToPosition(playerLastSeen);
    }

    void Update()
    {
        if (enemy.SeesPlayer())
        {
            enemy.ChangeState(shoot);
        }
        if (enemy.GetVelocity() == Vector3.zero)
        {
            enemy.ChangeState(idle);
        }
    }

    public override void End()
    {
    }
}
