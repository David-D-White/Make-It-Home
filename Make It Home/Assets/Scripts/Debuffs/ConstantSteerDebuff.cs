using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSteerDebuff : Debuff
{
    public Rigidbody car;
    public float steerAmount;
    [Range(0, 100)]
    public float swapChance;

    public override void debuff()
    {
        car.velocity += Vector3.right * steerAmount;
        if (Random.Range(0f, 100f) < swapChance)
            steerAmount *= -1;
    }

    public override void removeDebuff()
    {
    }
}
