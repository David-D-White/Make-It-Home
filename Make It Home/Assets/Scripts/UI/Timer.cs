using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLimit;
    public Text timer;


    private float time_;
    public float time
    {
        get {return time_; }
    }
    public float timeElapsed
    {
        get { return timeLimit - time;}
    }

    public bool running;

	void Start ()
    {
        time_ = timeLimit;
	}
	
	void Update ()
    {
        if (running)
        {
            time_ -= Time.deltaTime;
        }
        if (time_ <= 0)
            time_ = 0;
        int minutes = (int)(time / 60);
        int seconds = (int)((time - 60 * minutes) / 1);
        int miliseconds = (int)((time - 60 * minutes - seconds) / 0.01);
        displayTime(minutes, seconds, miliseconds);
	}

    void displayTime (int minutes, int seconds, int miliseconds)
    {
        string primary;
        string secondary;
        if (minutes != 0)
        {
            primary = formatTime(minutes);
            secondary = formatTime(seconds);
        }
        else
        {
            primary = formatTime(seconds);
            secondary = formatTime(miliseconds);
        }
        timer.text = "Time: [" + primary + ":" + secondary + "]";
    }

    string formatTime (int time)
    {
        if (time < 10)
            return "0" + time;
        else
            return "" + time;
    }
}
