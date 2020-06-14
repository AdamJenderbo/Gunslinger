using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1, 0));
        Debug.DrawRay(transform.position, new Vector3(-1, 0, 0), Color.green);
        if(hit.collider != null)
        {
            Debug.LogWarning("collision");
        }
        else
        {
            Debug.Log("no hit");
        }
    }
}
