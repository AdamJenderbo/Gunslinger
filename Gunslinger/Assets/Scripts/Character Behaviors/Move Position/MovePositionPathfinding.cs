using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionPathfinding : MonoBehaviour, IMovePosition
{
    private List<Vector3> path;
    private int pathIndex = -1;

    private IMoveVelocity moveVelocity;
    private ICharacterAim aim;

    [SerializeField]
    private Vector3 nextPathPosition;

    public void SetMovePosition(Vector3 movePosition)
    {
        path = Pathfinding.Instance.FindPath(transform.position, movePosition);
        if(path.Count > 0)
        {
            pathIndex = 0;
        }


    }

    void Start()
    {
        moveVelocity = GetComponent<IMoveVelocity>();
        aim = GetComponent<ICharacterAim>();
    }

    void Update()
    {
        if (pathIndex != -1)
        {
            nextPathPosition = path[pathIndex];
            Vector3 velocity = (nextPathPosition - transform.position).normalized;
            moveVelocity.SetVelocity(velocity);
            aim.SetTarget(nextPathPosition);
            if (Vector3.Distance(transform.position, nextPathPosition) < 0.1f)
            {
                pathIndex++;
                if(pathIndex >= path.Count)
                {
                    pathIndex = -1;
                }
            }
        }
        else
        {
            moveVelocity.SetVelocity(Vector3.zero);
        }
    }

    private void PrintPath()
    {
        if (path == null)
            return;
        foreach(Vector3 vec in path)
        {
            Debug.Log(vec.ToString());
        }
    }
}
