using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IdleState
{
    State shoot;

    public List<Vector3> waypoints;
    [SerializeField]
    int currentWaypoint;

    protected override void Awake()
    {
        base.Awake();
        shoot = GetComponent<ShootState>();

    }

    public override void Begin()
    {
        enemy.ToIdleView();
        currentWaypoint = 0;
        NextWaypoint();
    }

    void Update()
    {
        if(enemy.GetVelocity() == Vector3.zero)
        {
            NextWaypoint();
        }
        if(enemy.SeesPlayer())
        {
            enemy.ChangeState(shoot);
        }
    }

    public override void End()
    {
    }

    private void NextWaypoint()
    {
        currentWaypoint++;
        if (currentWaypoint >= waypoints.Count)
            currentWaypoint = 0;
        if (waypoints[currentWaypoint] == null)
            return;
        enemy.MoveToPosition(waypoints[currentWaypoint]);
    }

}
