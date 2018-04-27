using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPulsate : MonoBehaviour
{
    public RectTransform text;
    public float minScale, maxScale;
    [Range(0, 5)]
    public float rate;

    private Vector3 currentScale;
    private Vector3 modif;

    void Start()
    {
        currentScale = new Vector3 (1,1,1);
        modif = new Vector3(rate,rate,rate);
        if (currentScale.x > maxScale)
        {
            currentScale = currentScale.normalized * maxScale;
            modif *= -1;
        }
        else if (currentScale.x < minScale)
        {
            currentScale = currentScale.normalized * minScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentScale += modif * Time.deltaTime;
        if (currentScale.x <= minScale)
        {
            currentScale.x = minScale;
            modif *= -1;
        }
        else if (currentScale.x >= maxScale)
        {
            currentScale.x = maxScale;
            modif *= -1;
        }
        text.localScale= currentScale;
    }
}

