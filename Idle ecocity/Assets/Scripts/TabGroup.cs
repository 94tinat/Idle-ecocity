using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    //List of tabs
    public List<TabButton> tabs;

    //Tab selected
    public TabButton selectedTab;

    //List of tabs panels
    public List<GameObject> objectsToSwap;

    //Method to register each tab to the list
    public void AddTabsToList(TabButton tab)
    {
        if (tabs == null)
        {
            tabs = new List<TabButton>();
        }

        tabs.Add(tab);
    }

    //Method to select a tab
    public void OnTabSelected(TabButton tab)
    {
        //If is already selected a tab, deselect it
        if (selectedTab != null)
        {
            selectedTab.Deselect();

            //If the tab selected is the same as the previous active tab
            //hide the relative panel
            if (selectedTab == tab)
            {
                selectedTab = null;

                int index = tab.transform.GetSiblingIndex();
                objectsToSwap[index].SetActive(false);
            }

            //Else the selected tab will be the current tab and select its panel
            else
            {
                selectedTab = tab;
                selectedTab.Select();
                int index = tab.transform.GetSiblingIndex();
                for (int i = 0; i < objectsToSwap.Count; i++)
                {
                    objectsToSwap[i].SetActive(i == index);
                }
            }
  
        }

        //Else the selected tab will be the current tab and select its panel
        else
        {
            selectedTab = tab;
            selectedTab.Select();

            int index = tab.transform.GetSiblingIndex();
            for (int i = 0; i < objectsToSwap.Count; i++)
            {
                objectsToSwap[i].SetActive(i == index);
            }
        }
  
    }
}
