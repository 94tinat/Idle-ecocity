using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridHandler : MonoBehaviour, IDropHandler
{
    //The class of grid manager
    private GridManager gridManager;

    private void Start()
    {
        //Initialize the grid manager game object
        gridManager = GameObject.FindWithTag("gridManager").GetComponent<GridManager>();
    }

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

            //Get the tag of tile, that is the same of slot
            GameObject tileAnchored = GameObject.FindGameObjectsWithTag(tag)[1];

            Tile tileReference = tileAnchored.GetComponent<Tile>();

            //If the item in the slot is the solar panel
            //activate connections of tile where there's the panel
            if (item.gameObject.CompareTag("solarPanel"))

                ActivateConnections(tileReference, item);

        }       
    }

    //Method to activate connections of tile where there's the panel
    private void ActivateConnections(Tile tileReference, DragAndDropHandler item)
    {
        //The grid with tiles
        List<List<Tile>> grid = gridManager.grid;

        //For each row
        for (int j = 0; j < grid.Count; j++)
        {
            //For each tile in row
            for (int i = 0; i < grid.Count; i++)
            {
                //If the row contains the tile with panel
                //activate it and its connections
                if (grid[j].Contains(tileReference))
                {
                    grid[j][i].IsActived(true, "verticalNode");

                    //If tile is the reference tile
                    //activate the vertical and horizontal connections
                    //based on tile position
                    if (grid[j][i] == tileReference)
                    {
                        grid[j][i].IsActived(true, "horizontalNode");
                        switch (j)
                        {
                            case 0:
                                grid[j + 1][i].IsActived(true, "horizontalNode");
                                grid[j + 2][i].IsActived(true, "horizontalNode");
                                grid[j + 3][i].IsActived(true, "horizontalNode");
                                break;

                            case 1:
                                grid[j - 1][i].IsActived(true, "horizontalNode");
                                grid[j + 1][i].IsActived(true, "horizontalNode");
                                grid[j + 2][i].IsActived(true, "horizontalNode");
                                break;

                            case 2:
                                grid[j - 2][i].IsActived(true, "horizontalNode");
                                grid[j - 1][i].IsActived(true, "horizontalNode");
                                grid[j + 1][i].IsActived(true, "horizontalNode");
                                break;
                            case 3:
                                grid[j - 3][i].IsActived(true, "horizontalNode");
                                grid[j - 2][i].IsActived(true, "horizontalNode");
                                grid[j - 1][i].IsActived(true, "horizontalNode");
                                break;
                        }
                    }
                }
            }
        }

        item.tileReference = tileReference;
    }
}
