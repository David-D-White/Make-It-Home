using UnityEngine;
using UnityEngine.UI;

public class DrinkLevel : MonoBehaviour
{
    public Drinking drink;

    public Slider drinkLeft;
    public Slider drinkRight;

    private float maxLevel;

    // Use this for initialization
    void Start()
    {
        maxLevel = drink.drinksToEmpty;
    }

    private void Update()
    {
        float drinkAsPercent = drink.drinksToEmpty / maxLevel;
        Debug.Log(drinkAsPercent);

        drinkLeft.value = drinkAsPercent;
        drinkRight.value = drinkAsPercent;
    }
}
