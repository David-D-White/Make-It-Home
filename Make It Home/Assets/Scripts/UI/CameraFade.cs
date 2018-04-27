using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFade : MonoBehaviour
{
    public Image image;
    public Color fadeFrom;
    public Color fadeTo;
    [Range(0,1)] public float fadeRate;

    private Color currentColor;
    private float colorPos = 0;
    bool fading = false;

	void Start ()
    {
        currentColor = Color.Lerp(fadeFrom, fadeTo, 0);
        image.color = currentColor;
    }

	void Update ()
    {
		if (fading)
        {
            colorPos += Time.deltaTime * fadeRate;
            if (colorPos > 1)
                colorPos = 1;
            if (colorPos < 0)
                colorPos = 0;
            currentColor = Color.Lerp(fadeFrom, fadeTo, colorPos);
            image.color = currentColor;
        }
	}

    public void Fade ()
    {
        fading = true;
    }

    public void ReverseFade()
    {
        fadeRate *= -1;
        fading = true;
    }

    public void StopFade ()
    {
        fading = false;
    }

    public bool Faded ()
    {
        return colorPos == 1;
    }

    public bool Saturated()
    {
        return colorPos == 0;
    }
}
