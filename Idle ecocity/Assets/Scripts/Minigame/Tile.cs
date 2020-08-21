using UnityEngine;

public class Tile: MonoBehaviour
{
    bool supplied;

    bool withPanel;

    //Method to activate the tile connections, based on
    //type of connection (vertical or horizontal)
    public void SetActivated(bool activated, string type)
    {
        foreach (Transform child in transform)
        {
            if(child.CompareTag(type))
                child.gameObject.SetActive(activated);
        }
    }

    //Method to verify if two solar panels are crossed,
    //checking if the vertical and horizontal nodes of a tile are active
    public bool AreActivated()
    {
        bool verticalActive = false;
        bool horizontalActive = false;
        foreach (Transform child in transform)
        {
            if (child.CompareTag("verticalNode") && child.gameObject.activeSelf)
                verticalActive = true;
            else if (child.CompareTag("horizontalNode") && child.gameObject.activeSelf)
                horizontalActive = true;
        }

        if (verticalActive && horizontalActive) return true;
        else return false;
    }

    //Method to check if a tile has a house and put a flag
    public bool HasHouse()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("house"))
            {
                supplied = true;
                return true;
            }
        }

        return false;
    }

    //Method to put a flag in a tile, if this has a panel 
    public void HasPanel(bool panel)
    {
        withPanel = panel;
    }

    //Method to check if a tile has a panel
    public bool CheckPanel()
    {
        return withPanel;
    }

    //Method to check if a house is supplied
    public bool CheckSupply()
    {
        return supplied;
    }

}
