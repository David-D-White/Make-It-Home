using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopIn : MonoBehaviour
{
    public RectTransform Element;
    [Range (0,5)] public float popRate;

    private bool popping;
    private float currentSize;

	void Start ()
    {
        currentSize = 0;
	}
	
	void Update ()
    {
        if (popping)
            currentSize += popRate * Time.deltaTime;
        if (currentSize > 1)
            currentSize = 1;
        else if (currentSize < 0)
            currentSize = 0;
        Element.localScale = Vector3.one * currentSize;
	}

    public void popIn ()
    {
        if (popRate < 0)
            popRate *= -1;
        popping = true;
    }

    public void popOut ()
    {
        if (popRate > 0)
            popRate *= -1;
        popping = true;
    }

    public void stopPop ()
    {
        popping = false;
    }

    public bool poppedIn()
    {
        return currentSize == 1;
    }

    public bool poppedOut()
    {
        return currentSize == 0;
    }
}
