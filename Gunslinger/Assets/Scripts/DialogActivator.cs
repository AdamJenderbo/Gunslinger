using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    public string[] dialogLines;

    private bool playerInside;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInside)
        {
            if(Input.GetKeyDown(KeyCode.E) && !DialogManager.instance.ShowingDialog)
            {
                DialogManager.instance.ShowDialog("Sheriff", dialogLines);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
            playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            playerInside = false;
    }
}
