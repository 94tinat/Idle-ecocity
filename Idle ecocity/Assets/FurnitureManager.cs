using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureManager : MonoBehaviour
{
	//Class of idle manager
	private IdleManager idle;

	//Show number of coins earned per second by a furniture
	public Text coinsPerSecText;

	//Show upgrade forniture
	public Text upgradeFurnitureText;

	//Show cost of furniture
	public Text costFurnitureText;

	//Represent the levels bar of a furniture 
	public Image upgradeBar;

	//Represent the livability popup when the livability bar increases by 10%
	public GameObject livabilityPopup;

	//Represent the autoplay enabled when a furniture is upgraded
	public GameObject productionProgress;

	//Represent upgrade's cost of a furniture
	public double upgradeCostFurniture;

	//Represent upgrade's level of a furniture
	public int upgradeLevelFurniture;

	//Represent number of coins earned per second by a furniture
	public double coinsPerSec;

	//Represent the number of levels after which the number of coins/second increase
	public int levelThresholdUpgrade;

	//These represent how the coins/second function increases: after the levels threshold ->
	//incrementUpgrade = incrementUpgrade + rateUpgrade
	public double rateUpgrade;
	public double incrementUpgrade;

    void Start()
    {
		//Initialize the idle manager game object
		idle = GameObject.FindWithTag("idleManager").GetComponent<IdleManager>();

	}

	void Update()
    {
		//If the upgrade's level of furniture is less than a constant upgrade based on the type of furniture
		//update coins/second with upgrade's level of a furniture
		if (upgradeLevelFurniture <= levelThresholdUpgrade)
			coinsPerSec = upgradeLevelFurniture;

		//Else increment coins/second with a gradual increment based on the type of furniture
		else
			coinsPerSec = upgradeLevelFurniture * incrementUpgrade;

		//Update text of number of coins earned
		idle.coinsFurnitureText.text = "+" + idle.ChangeNumber(idle.coinsFurniture);

		//Update text of coins/second by a furniture
		coinsPerSecText.text = idle.ChangeNumber(coinsPerSec) + " coins/s";

		//Update text of furniture upgraded
		upgradeFurnitureText.text = "UPGRADE\nLevel: " + upgradeLevelFurniture;

		//Show upgraded cost of furniture
		costFurnitureText.text = "- " + idle.ChangeNumber(upgradeCostFurniture);

		//Update the value of coins earned by a furniture with the value of coins/second
		//(Time.deltaTime to calculate the time in milliseconds between two frames, to guarantee
		//one coin per second)
		idle.coinsFurniture += coinsPerSec * Time.deltaTime;

		//If the progression bar of a furniture is enabled,
		//fill the bar gradually, based on level of furniture
		if (productionProgress.activeSelf)
		{
			Image progressionBar = productionProgress.transform.GetChild(1).transform.GetComponent<Image>();

			if (progressionBar.fillAmount > 0.99)
				progressionBar.fillAmount = 0;

			else
				progressionBar.fillAmount += (float) upgradeLevelFurniture / 1000;

		}

	}

	//Method to upgrade a furniture
	public void BuyUpgradeFurniture()
	{
		//If the total number of coins is enough to buy an upgrade:
		//- disable the button about to manually earn coins
		//- enable the progression bar about autoplay of furniture
		//- increase the upgrade's level of furniture
		//- decrease the total number of coins based on cost of upgrade
		//- increase the cost of upgrade of a constant equal to 1.07
		//(based on some of the most popular idle games)
		//- increase the upgrade bar of levels
		if (idle.ecoCoins >= upgradeCostFurniture)
		{
			productionProgress.SetActive(true);
			idle.clickToEarn.gameObject.SetActive(false);

			upgradeLevelFurniture++;
			idle.ecoCoins -= upgradeCostFurniture;
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
				upgradeBar.fillAmount += (float)1 / levelThresholdUpgrade;


			//Each 5 levels of upgrade the livability bar increases by 10%
			//and show a popup to notify this (lasting 1 sec)
			if (upgradeLevelFurniture >= 5 && upgradeLevelFurniture % 5 == 0)
			{
				idle.livabilityBar.fillAmount += 0.1f;
				livabilityPopup.SetActive(true);
				StartCoroutine(WaitForPopup());

			}

		}

	}

	//Interface to start a coroutine about the livability popup,
	//when the livability bar increases
	IEnumerator WaitForPopup()
	{
		yield return new WaitForSeconds(1);

		livabilityPopup.SetActive(false);

	}

}
