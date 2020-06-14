using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterAim
{
    Vector3 GetAimDir();

    float GetAimAngle();

    void SetTarget(Vector3 target);
}
