using UnityEngine;
using UnityEngine.UI;

public class IdleManager : MonoBehaviour
{
	//Show total number of coins
	public Text ecoCoinsText;

	//Show number of coins earned by a furniture
	public Text coinsFurnitureText;

	//Show number of coins earned per second by a furniture
	public Text coinsPerSecText;

	//Show upgrade forniture
	public Text upgradeFurnitureText;

	//Represent total number of coins
	public double ecoCoins;

	//Represent number of coins earned by a furniture
	public double coinsFurniture;

	//Represent number of coins earned per second by a furniture
	public double coinsPerSec;

	//Represent upgrade's cost of a furniture
	public double upgradeCostFurniture;

	//Represent upgrade's level of a furniture
	public int upgradeLevelFurniture;

    //Represent the levels bar of a furniture 
	public Image upgradeBar;

	//Represent the number of levels after which the number of coins/second increase
	public int levelThresholdUpgrade;

	//These represent how the coins/second function increases: after the levels threshold ->
	//incrementUpgrade = incrementUpgrade + rateUpgrade
	public double rateUpgrade;

	public double incrementUpgrade;

	void Start()
    {

	}

	void Update()
    {
		//If the upgrade's level of furniture is less than a constant upgrade based on the type of furniture
		//Update coins/second with upgrade's level of a furniture
		if (upgradeLevelFurniture <= levelThresholdUpgrade)
			coinsPerSec = upgradeLevelFurniture;
        //Else increment coins/second with a gradual increment based on the type of furniture
		else
			coinsPerSec = upgradeLevelFurniture * incrementUpgrade;

        //Update text of number of coins earned (F0 is used to not have decimals in the text) 
        coinsFurnitureText.text = "+" + coinsFurniture.ToString("F0");

        //Update text of total number of coins
		ecoCoinsText.text = "" + ecoCoins.ToString("F0");

		//Update text of coins/second by a furniture
		coinsPerSecText.text  = coinsPerSec.ToString("F0") + " coins/s";

		//Update text of furniture upgraded
		upgradeFurnitureText.text = "Furniture Upgrade \nCost: " + upgradeCostFurniture.ToString("F0") + " coins\nClick\nLevel: " + upgradeLevelFurniture;

		//Update the value of coins earned by a furniture with the value of coins/second
		//(Time.deltaTime to calculate the time in milliseconds between two frames, to guarantee
        //one coin per second)
		coinsFurniture += coinsPerSec * Time.deltaTime;

	}

	//Method to earn coins by a furniture, clicking on it
	public void ClickToIncreaseCoins()
    {
        //If upgrade's level of a furniture is 0 (so it's still early),
        //increase number of coins by hand, clicking on button
        if(upgradeLevelFurniture == 0)
		{
			coinsFurniture += 1;
            //Total number of coins is equal to coins earned by a furniture
			ecoCoins = coinsFurniture;
		}

        //Else update total number of coins with coins earned by a furniture
        //and reset the coins of the furniture
        else
		{
			ecoCoins += coinsFurniture;
			coinsFurniture = 0;
		}
	}

    //Method to upgrade a furniture
	public void BuyUpgradeFurniture()
	{
        //If the total number of coins is enough to buy an upgrade:
        //- increase the upgrade's level of furniture
        //- decrease the total number of coins based on cost of upgrade
        //- increase the cost of upgrade of a constant equal to 1.07
        //(based on some of the most popular idle games)
        //- increase the upgrade bar of levels
		if (ecoCoins >= upgradeCostFurniture)
		{
			upgradeLevelFurniture++;
			ecoCoins -= upgradeCostFurniture;
			upgradeCostFurniture *= 1.07;

            //If bar's amount is bigger than 0.95
            //the bar is reset and the increment upgrade is added up by a percentage
			if (upgradeBar.fillAmount > 0.95)
			{
				upgradeBar.fillAmount = 0;
				incrementUpgrade += rateUpgrade;
			}

            //Else the bar increases of a fraction based on constant upgrade
			else
				upgradeBar.fillAmount += (float) 1 / levelThresholdUpgrade;
		}

	
	}

}
