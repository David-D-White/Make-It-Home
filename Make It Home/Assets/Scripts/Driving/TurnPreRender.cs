using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPreRender : MonoBehaviour
{
    public GameObject right, left;
    public enum Side
    {
        right, left
    }

    public void enable(Side side)
    {
        switch (side)
        {
            case Side.left:
                left.SetActive(true);
                break;
            case Side.right:
                right.SetActive(true);
                break;
        }
    }

    public void disable()
    {
        right.SetActive(false);
        left.SetActive(false);
    }
 } 
