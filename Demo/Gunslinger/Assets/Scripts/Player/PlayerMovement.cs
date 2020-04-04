using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Player player;
    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = +1f;
        }

        moveDir = new Vector3(moveX, moveY).normalized;

        bool isIdle = moveX == 0 && moveY == 0;
        if (isIdle)
        {
            player.view.PlayIdleAnim();
        }
        else
        {
            //lastMoveDir = moveDir;
            player.view.PlayMoveAnim(moveDir);
        }
    }

    private void FixedUpdate()
    {
        player.PlayerRigidbody2D.velocity = moveDir * player.Speed;
    }
}
