using System.Collections;
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

	//Represent the button when the furniture is locked
	public Button lockFurniture;

	//Represent the button when the furniture upgrade is locked
	public Button lockUpgrade;

	//Represent the button when the furniture upgrade is unlocked
	public Button unlockUpgrade;

	//Represent the game object when the furniture is unlocked
	public GameObject unlockFurniture;

	public GameObject autoplay;

	//Represent the bar of livability
	public Image livabilityBar;

	//Represent the image to go to next area
	public Image nextAreaLock;

	//Represent the autoplay enabled when a furniture is upgraded
	public Image progressionBar;

	//Represent total number of coins
	public double ecoCoins;

	//Represent number of coins earned by a furniture
	public double coinsFurniture;

	//Represent boolean if a furniture is locked
	private bool lockedFurniture;

	//The threshold after which the minigame is unlocked
	public float livabilityThreshold;

    private void Start()
    {
		//Load the variables
		Load();
	}

	void Update()
    {

		//Update text of number of coins earned (F0 is used to not have decimals in the text) 
		coinsFurnitureText.text = "+" + ChangeNumber(coinsFurniture);

		//Update text of total number of coins
		ecoCoinsText.text = "" + ChangeNumber(ecoCoins);

		//If the livability bar is bigger than a threshold
		//the minigame is activated
		if(livabilityBar.fillAmount >= livabilityThreshold)
			nextAreaLock.gameObject.SetActive(false);

		StartCoroutine(Save());

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

		//Reset the coins of the furniture and the progression bar
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

	//Method to load the minigame scene
	public void GoToMinigame(string minigame)
	{
		SceneManager.LoadScene(minigame);
	}

	//Method to load variables
	public void Load()
    {
		//Load the last value saved (initialized to initial value)
		ecoCoins = double.Parse(PlayerPrefs.GetString("ecoCoins", "0"));
		coinsFurniture = double.Parse(PlayerPrefs.GetString("coinsFurniture", "0"));
		lockedFurniture = bool.Parse(PlayerPrefs.GetString("lockFurniture", true.ToString()));
		livabilityBar.fillAmount = PlayerPrefs.GetFloat("livabilityBar", 0f);

		//Once unlocked a furniture must be unlocked (also the upgrade)
		if (lockedFurniture == false)
        {
			lockFurniture.gameObject.SetActive(false);
			lockUpgrade.gameObject.SetActive(false);
			unlockFurniture.SetActive(true);
			unlockUpgrade.gameObject.SetActive(true);
			
		}

	}

	//Method to save variables, using a coruoutine to save every 3 seconds
	IEnumerator Save()
    {
		yield return new WaitForSeconds(3);

		//Set the current value of coins and coins earn by a furniture
		PlayerPrefs.SetString("ecoCoins", ecoCoins.ToString());
		PlayerPrefs.SetString("coinsFurniture", coinsFurniture.ToString());
		PlayerPrefs.SetString("lockFurniture", false.ToString());
		PlayerPrefs.SetFloat("livabilityBar", livabilityBar.fillAmount);
	}

}
