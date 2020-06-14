using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput_Mouse : MonoBehaviour, IMovementInput
{
    IMovePosition movePosition;

    void Start()
    {
        movePosition = GetComponent<IMovePosition>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            movePosition.SetMovePosition(GetMouseWorldPosition());
        }
    }


    private Vector3 GetMouseWorldPosition()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0f;
        return worldPosition;
    }
}
