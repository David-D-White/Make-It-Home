using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score;
    public Timer timer;
    private int scoreVal_;
    public int scoreVal
    {
        get { return scoreVal_; }
    }

	void Update ()
    {
        scoreVal_ = (int)(timer.timeElapsed*Data.numDrinks);
        score.text = scoreVal_ + "";
	}
}
