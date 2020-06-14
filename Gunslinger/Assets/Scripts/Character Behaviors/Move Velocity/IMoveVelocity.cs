using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveVelocity
{
    Vector3 GetVelocity();
    void SetVelocity(Vector3 velocity);
}
