using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionDirect : MonoBehaviour, IMovePosition
{
    private Vector3 movePosition;
    private IMoveVelocity moveVelocity;

    public void SetMovePosition(Vector3 movePosition)
    {
        this.movePosition = movePosition;
    }

    private void Start()
    {
        moveVelocity = GetComponent<IMoveVelocity>();
    }

    void Update()
    {
        Vector3 moveDir = (movePosition - transform.position).normalized;
        moveVelocity.SetVelocity(moveDir);
    }
}
