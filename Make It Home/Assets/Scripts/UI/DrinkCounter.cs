using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkCounter : MonoBehaviour
{
    public Text numDrinks;

	void Update ()
    {
        numDrinks.text = "" + Data.numDrinks;
	}
}
