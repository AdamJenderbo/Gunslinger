using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticState : IdleState
{
    State shoot;


    protected override void Awake()
    {
        base.Awake();
        shoot = GetComponent<ShootState>();
    }

    public override void Begin()
    {
        enemy.ToIdleView();

    }


    void Update()
    {
        enemy.Stop();
        if (enemy.SeesPlayer())
        {
            enemy.ChangeState(shoot);
        }
    }

    public override void End()
    {
    }

}
