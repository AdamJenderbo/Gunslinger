using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    bool playerEntered;
    protected Player player;
    public Action<Player> onTriggerAction;

    void Update()
    {
        if (playerEntered)
        {
            onTriggerAction(player);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerEntered = true;

            if (player == null)
            {
                player = other.GetComponent<Player>();
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerEntered = false;
        }
    }
}
