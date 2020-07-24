using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler,
    IDragHandler
{
    //Item to drag
    private RectTransform rectTransform;

    //Canvas of item
    private CanvasGroup canvasGroup;

    //Method to initialize variables before the Start
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //Implementation of interface related to detect the begin of drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("On begin");

        //While is dragged, the item is more trasparent
        canvasGroup.alpha = .6f;

        //Raycast goes through this item, allowing to be detected
        canvasGroup.blocksRaycasts = false;
    }

    //Implementation of interface related to detect the drag
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");

        //Anchor position of item and change it, based on a delta,
        //adding the amount of mouse movement with that of the previous frame
        rectTransform.anchoredPosition += eventData.delta; 

    }

    //Implementation of interface related to detect the end of drag
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("On end");

        canvasGroup.alpha = 1f;

        //Return to block raycasts through this item
        canvasGroup.blocksRaycasts = true;

    }

    //Implementation of interface related to detect the press of item
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointdown");
    }

}
