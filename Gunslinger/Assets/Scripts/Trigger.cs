using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    bool playerEntered;
    protected Player player;
    public Action<Player> onTriggerAction;

    // Update is called once per frame
    void Update()
    {
        if(playerEntered)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                onTriggerAction(player);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerEntered = true;

            if(player == null)
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
