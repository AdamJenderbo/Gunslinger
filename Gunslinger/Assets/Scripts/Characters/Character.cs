using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [Header("Movement")]

    public Rigidbody2D rigidBody;
    public float moveSpeed;


    protected Vector2 moveDirection;

    protected void Move()
    {
        rigidBody.velocity = moveDirection * moveSpeed;
    }

    protected void Stop()
    {
        rigidBody.velocity = Vector2.zero;
    }

}
