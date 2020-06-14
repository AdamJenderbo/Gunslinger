using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput_Keys : MonoBehaviour, IMovementInput
{
    Player player;

    IMoveVelocity moveVelocity;

    void Start()
    {
        player = GetComponent<Player>();
        moveVelocity = GetComponent<IMoveVelocity>();
    }

    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) moveY = +1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) moveY = -1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveX = +1f;

        moveVelocity.SetVelocity(new Vector3(moveX, moveY).normalized);
    }
}
