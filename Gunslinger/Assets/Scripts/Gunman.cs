using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunman : MonoBehaviour
{
    public GameObject frontHand;
    public GameObject backHand;
    protected Transform gunHand;

    public Gun gun;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        gunHand = gun.hand == Gun.Hand.singleHand ? frontHand.transform : backHand.transform; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchGun(Gun gun)
    {
        // drop current gun
        this.gun.Drop();

        // add new gun
        Gun gunInstance = Instantiate(gun);

        if (gun.hand == Gun.Hand.singleHand)
        {
            gunHand = frontHand.transform;
            frontHand.SetActive(true);
        }
        else if (gun.hand == Gun.Hand.doubleHand)
        {
            gunHand = backHand.transform;
            frontHand.SetActive(false);
        }

        gunInstance.transform.parent = gunHand.transform;
        gunInstance.transform.position = gunHand.position;
        gunInstance.transform.localRotation = Quaternion.Euler(Vector3.zero);
        gunInstance.transform.localScale = Vector3.one;

        this.gun = gunInstance;

    }


}
