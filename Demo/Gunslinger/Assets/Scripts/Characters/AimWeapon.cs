using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimWeapon : MonoBehaviour
{
    private Transform aimTransform;
    private Camera cam;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        aimTransform = transform.Find("Aim");
        cam = Camera.main;
        player = GetComponentInChildren<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        aimTransform.rotation = Quaternion.Euler(0, 0, angle);

        if (angle > 150 || angle < -150)
            player.TurnLeft();
        else if (angle > 30)
            player.TurnBackward();
        else if (angle < -30)
            player.TurnForward();
        else
            player.TurnRight();

        Vector3 localScale = Vector3.one;
        localScale.y = (angle > 90 || angle < -90) ? -1f : 1f;
        aimTransform.localScale = localScale;




    }
}
