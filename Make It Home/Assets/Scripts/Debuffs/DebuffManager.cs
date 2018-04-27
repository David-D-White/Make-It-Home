using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffManager : MonoBehaviour
{
    public Debuff [] debuffs;

	public void debuff (float debuffTime)
    {
        int debuffIndex = Random.Range(0,debuffs.Length);
        if (debuffs[debuffIndex].applied)
        {
            foreach (Debuff d in debuffs)
            {
                if (!d.applied)
                {
                    d.applyDebuff(debuffTime);
                    break;
                }
            }
        }
        else
        {
            debuffs[debuffIndex].applyDebuff(debuffTime);
        }
    }
}
