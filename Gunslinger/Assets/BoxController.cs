using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{

    public Vector3 moveTo;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<IMovePosition>().SetMovePosition(moveTo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
