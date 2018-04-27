using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff : MonoBehaviour
{
    private float timeRemaining;

    public bool applied
    {
        get { return timeRemaining > 0;}
    }

    void LateUpdate()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            debuff();
        }
        else
        {
            removeDebuff();
        }

    }

    public abstract void debuff();

    public abstract void removeDebuff();

    public void applyDebuff(float time)
    {
        timeRemaining = time;
    }
}
