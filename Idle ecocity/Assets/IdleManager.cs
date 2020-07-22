using UnityEngine;
using UnityEngine.UI;

public class IdleManager : MonoBehaviour
{
    //Show number of coins earned
    public Text ecoCoinsText;

    //Represent accurate number of coins
    public double ecoCoins;

    void Start()
    {
        //Initialize coins
        ecoCoins = 0;
    }

    void Update()
    {
        //Update number of coins earned 
        ecoCoinsText.text = "Coins:  " + ecoCoins;
    }

    //Method to earn by furniture
    public void ClickToIncrease()
    {
        ecoCoins += 1;
    }
}
