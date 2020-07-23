using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TabButton : MonoBehaviour, IPointerClickHandler
{
    //Group of tabs
    public TabGroup tabs;

    //Event of tab selection
    public UnityEvent onTabSelected;

    //Event of tab deselection
    public UnityEvent onTabDeselected;

    void Start()
    {
        //Initialize the list
        tabs.AddTabsToList(this);
    }

    //Method to select a tab invoking the selection event of tabs in TabGroup.cs,
    //where are specified behaviours
    public void Select()
    {

        if (onTabSelected != null)
        {
            onTabSelected.Invoke();
        }
    }

    //Method to deselect a tab invoking the deselection event of tabs in TabGroup.cs
    public void Deselect()
    {

        if (onTabSelected != null)
        {
            onTabDeselected.Invoke();
        }
    }

    //Implementation of interface related to event system of a tab click
    public void OnPointerClick(PointerEventData eventData)
    {
        tabs.OnTabSelected(this);

    }

}