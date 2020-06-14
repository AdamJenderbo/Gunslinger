using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAim : MonoBehaviour, ICharacterAim
{
    private Vector3 target;
    private Vector3 aimDir;
    private float angle;
    private Transform gunHand;


    Character character;

    public Vector3 GetAimDir()
    {
        return aimDir;
    }

    public float GetAimAngle()
    {
        return angle;
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
        gunHand = transform.Find("Aim");
        angle = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDir = new Vector2(target.x - transform.position.x, target.y - transform.position.y).normalized;
        aimDir = targetDir;
        angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        if (angle > 90 || angle < -90) { gunHand.localScale = new Vector3(1f, -1f, 1f); }
        else { gunHand.localScale = new Vector3(-1f, 1f, 1f); }

        if (angle > 90 || angle < -90) { transform.localScale = Vector3.one; }
        else { transform.localScale = new Vector3(-1f, 1f, 1f);  }

        gunHand.rotation = Quaternion.Euler(0, 0, angle);



    }
}
