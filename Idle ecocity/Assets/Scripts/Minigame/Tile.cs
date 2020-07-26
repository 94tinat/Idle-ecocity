using UnityEngine;

public class Tile: MonoBehaviour
{
    //Method to activate the tile connections, based on
    //type of connection (vertical or horizontal)
    public void IsActived(bool activated, string type)
    {
        foreach (Transform child in transform)
        {
            if(child.CompareTag(type))
                child.gameObject.SetActive(activated);
        }

    }

}
