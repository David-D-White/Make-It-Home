using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSensitivityDebuff : Debuff
{
    public float sensitivityMult;
    public Rigidbody car;

    public override void debuff()
    {
        car.velocity *= sensitivityMult;
    }

    public override void removeDebuff()
    {
    }
}
