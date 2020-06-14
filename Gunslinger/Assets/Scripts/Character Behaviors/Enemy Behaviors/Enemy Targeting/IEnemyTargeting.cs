using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyTargeting
{
    float GetFOV();

    float GetViewDistance();

    bool SeesPlayer();
}
