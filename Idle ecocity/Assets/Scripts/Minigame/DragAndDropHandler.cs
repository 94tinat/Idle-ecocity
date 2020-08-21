using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler,
    IDragHandler
{
    //Item to drag
    private RectTransform rectTransform;

    //Canvas of item
    private CanvasGroup canvasGroup;

    //Boolean to check if the item is dropped
    public bool validDrop;

    //The initial position of item
    private Vector2 startingPosition;

    //The class of grid manager
    private GridManager gridManager;

    private GridHandler gridHandler;

    //The reference tile where there's the panel
    public Tile referenceTile;

    //Method to initialize variables before the Start
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }

    private void Start()
    {
        //Save the initial position of item
        startingPosition = GetComponent<RectTransform>().localPosition;

        //Initialize the grid manager game object
        gridManager = GameObject.FindWithTag("gridManager").GetComponent<GridManager>();

        //Initialize the grid handler game object
        gridHandler= GameObject.FindWithTag("gridManager").GetComponent<GridHandler>();
    }

    //Implementation of interface related to detect the begin of drag
    public void OnBeginDrag(PointerEventData eventData)
    {

        //While is dragged, the item is more trasparent
        canvasGroup.alpha = .6f;

        //Raycast goes through this item, allowing to be detected
        canvasGroup.blocksRaycasts = false;

        if(validDrop)
        {
            //Disable connections of tile where there's the panel
            DeactivateConnections();

            validDrop = false;
        }
        

    }

    //Implementation of interface related to detect the drag
    public void OnDrag(PointerEventData eventData)
    {

        //Anchor position of item and change it, based on a delta,
        //adding the amount of mouse movement with that of the previous frame
        rectTransform.anchoredPosition += eventData.delta; 

    }

    //Implementation of interface related to detect the end of drag
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;

        //Return to block raycasts through this item
        canvasGroup.blocksRaycasts = true;

        //If the item isn't dropped
        //set it to the initial position and increase count items
        if (!validDrop)
        {
            validDrop = false;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = startingPosition;
        }

    }

    //Method to deactivate connections of tile where there's the panel
    private void DeactivateConnections()
    {
        //The grid with tiles
        List<List<Tile>> grid = gridManager.grid;

        referenceTile.HasPanel(false);

        //For each row
        for (int j = 0; j < grid.Count; j++)
        {
            //For each tile in row
            for (int i = 0; i < grid.Count; i++)
            {
                //If the row contains the tile with panel
                //deactivate it and its connections
                if (grid[j].Contains(referenceTile))
                {
                    grid[j][i].SetActivated(false, "verticalNode");

                    //If tile is the reference tile
                    //deactivate the vertical and horizontal connections
                    //based on tile position and check if there is houses in tiles
                    if (grid[j][i] == referenceTile)
                    {
                        grid[j][i].SetActivated(false, "horizontalNode");
                        switch (j)
                        {
                            case 0:
                                grid[j + 1][i].SetActivated(false, "horizontalNode");
                                grid[j + 1][i].HasHouse();

                                grid[j + 2][i].SetActivated(false, "horizontalNode");
                                grid[j + 2][i].HasHouse();

                                grid[j + 3][i].SetActivated(false, "horizontalNode");
                                grid[j + 3][i].HasHouse();

                                break;

                            case 1:
                                grid[j - 1][i].SetActivated(false, "horizontalNode");
                                grid[j - 1][i].HasHouse();

                                grid[j + 1][i].SetActivated(false, "horizontalNode");
                                grid[j + 1][i].HasHouse();

                                grid[j + 2][i].SetActivated(false, "horizontalNode");
                                grid[j + 2][i].HasHouse();

                                break;

                            case 2:
                                grid[j - 2][i].SetActivated(false, "horizontalNode");
                                grid[j - 2][i].HasHouse();

                                grid[j - 1][i].SetActivated(false, "horizontalNode");
                                grid[j - 1][i].HasHouse();

                                grid[j + 1][i].SetActivated(false, "horizontalNode");
                                grid[j + 1][i].HasHouse();

                                break;

                            case 3:
                                grid[j - 3][i].SetActivated(false, "horizontalNode");
                                grid[j - 3][i].HasHouse();

                                grid[j - 2][i].SetActivated(false, "horizontalNode");
                                grid[j - 2][i].HasHouse();

                                grid[j - 1][i].SetActivated(false, "horizontalNode");
                                grid[j - 1][i].HasHouse();

                                break;
                        }
                    }
                }
            }
        }

        //If any house is supplied, decrement the number
        if(GridHandler.countSuppliedHouses > 0)
        {
            foreach (List<Tile> list in grid)
            {
                foreach (Tile t in list)
                {
                    if (t.CheckSupply()) GridHandler.countSuppliedHouses--;
                }
            }
        }
    }
}
