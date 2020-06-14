using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Shooting shooting;

    // Start is called before the first frame update
    void Start()
    {
        shooting = GetComponent<Shooting>();
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            shooting.HoldShoot();
        }

        if (Input.GetMouseButtonDown(0))
        {

            shooting.Shoot();
        }
    }
}
