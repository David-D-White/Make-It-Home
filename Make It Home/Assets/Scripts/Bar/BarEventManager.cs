using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarEventManager : MonoBehaviour
{
    public BarEvent [] events;
    public Timer timer;
    public CameraFade camFade;
    public MusicFade musicFade;

    private BarEvent currentEvent;
    private GameObject currentObject;
    private Transform parentTransform;

    // Use this for initialization
    void Start ()
    {
        parentTransform = GetComponent<Transform>();
        newEvent();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (timer.time != 0)
        {
            if (currentEvent.completed())
            {
                if (currentEvent.active && !currentEvent.animating())
                {
                    currentEvent.animateOut(parentTransform);
                }
                else if (!currentEvent.active)
                {
                    Data.numDrinks++;
                    Destroy(currentObject);
                    newEvent();
                }
            }
        }
        else
        {
            if (camFade.Saturated())
            {
                camFade.Fade();
                musicFade.fadeOut();
            }
            else if (camFade.Faded())
            {
                camFade.StopFade();
                SceneManager.LoadScene("Road");
            }
        }
	}

    void newEvent()
    {
        int index = UnityEngine.Random.Range(0, events.Length - 1);
        currentObject = GameObject.Instantiate<GameObject>(events[index].gameObject, parentTransform.position, parentTransform.rotation);
        currentEvent = currentObject.GetComponent<BarEvent>();
        currentEvent.animateIn(parentTransform);
        currentEvent.active = true;
    }
}
