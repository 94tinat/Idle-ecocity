using UnityEngine;
using UnityEngine.EventSystems;

public class GridHandler : MonoBehaviour, IDropHandler
{

    //Implementation of interface related to detect the item drop in grid
    public void OnDrop(PointerEventData eventData)
    {
        //Instanziate drag and drop handler to check if the item is dropped
        DragAndDropHandler item = eventData.pointerDrag.GetComponent<DragAndDropHandler>();

        //The item dropped in the grid is in the event data, so if it was
        //released, is set the anchor position to this position
        if (eventData.pointerDrag != null)
        {
            //The item is dropped
            item.validDrop = true;

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                GetComponent<RectTransform>().anchoredPosition;

        }

    }

}
