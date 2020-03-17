using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown
{
    private float duration;
    private float value;

    public Cooldown(float duration)
    {
        this.duration = duration;
        value = this.duration;
    }

    public void Reset()
    {
        value = duration;
    }

    public void Update()
    {
        value -= Time.deltaTime;
    }

    public bool Done()
    {
        return value <= 0;
    }
}
