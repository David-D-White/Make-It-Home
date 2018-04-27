using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFade : MonoBehaviour
{
    public AudioSource source;
    [Range(0,1)]public float fadeRate;
    [Range(0,1)]public float maxVolume;

    private bool fading;

	void Update ()
    {
        if (fading)
        {
            if (fadeRate > 0)
            {
                source.volume += fadeRate * Time.deltaTime;
                if (source.volume >= maxVolume)
                {
                    source.volume = maxVolume;
                    fading = false;
                }
            }
            else
            {
                source.volume += fadeRate * Time.deltaTime;
                if (source.volume <= 0)
                {
                    source.volume = 0;
                    fading = false;
                }
            }
        }
	}

    public void fadeIn()
    {
        fading = true;
        source.volume = 0;
        if (fadeRate < 0)
            fadeRate *= -1;
    }

    public void fadeOut()
    {
        fading = true;
        source.volume = maxVolume;
        if (fadeRate > 0)
            fadeRate *= -1;
    }
}
