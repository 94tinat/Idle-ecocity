using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IdleManager : MonoBehaviour
{
	//Show total number of coins
	public Text ecoCoinsText;

	//Show number of coins earned by a furniture
	public Text coinsFurnitureText;

	//Represent the button about to manually earn coins 
	public Button clickToEarn;

	//Represent the bar of livability
	public Image livabilityBar;

	public Image nextAreaLock;

	//Represent the autoplay enabled when a furniture is upgraded
	public Image progressionBar;

	//Represent total number of coins
	public double ecoCoins;

	//Represent number of coins earned by a furniture
	public double coinsFurniture;

    void Update()
    {

		//Update text of number of coins earned (F0 is used to not have decimals in the text) 
		coinsFurnitureText.text = "+" + ChangeNumber(coinsFurniture);

		//Update text of total number of coins
		ecoCoinsText.text = "" + ChangeNumber(ecoCoins);

		if(livabilityBar.fillAmount >= 0.8)
			nextAreaLock.gameObject.SetActive(false);

	}

	//Method to earn coins manually by a furniture, clicking on it
	public void ClickToIncreaseCoins()
	{
		coinsFurniture += 1;

		//Total number of coins is equal to coins earned by a furniture
		ecoCoins = coinsFurniture;
	}

	//Method to update the total number of coins with coins earned by a furniture
	public void AutoPlayFurniture()
    {
		ecoCoins += coinsFurniture;

		//Reset the coins of the furniture
		coinsFurniture = 0;

		progressionBar.fillAmount = 0;


	}

	//Method to change the value of numerical texts and reduce their length
	public string ChangeNumber(double amount)
	{
		string value;

		if (amount >= 1000000)
			value = (amount / 1000000).ToString("F1") + "M";

		else if (amount >= 1000)
			value = (amount / 1000).ToString("F1") + "K";

		else
			value = amount.ToString("F0");

		return value;
	}

	public void GoToMinigame(string minigame)
	{
		SceneManager.LoadScene(minigame);
	}

}
