using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    ICharacterAim aim;

    void Start()
    {
        aim = GetComponent<ICharacterAim>();
    }

    void Update()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aim.SetTarget(target);
    }
}
