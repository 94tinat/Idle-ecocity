using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridHandler : MonoBehaviour, IDropHandler
{
    //The class of grid manager
    private GridManager gridManager;

    //Boolean to check if it's present a house in a tile
    private bool flag;

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
            //Initialize boolean to false
            flag = false;

            //If there is already a solar panel in the tile,
            //the item can't be positioned in that tile, so
            //it's not a valid drop and the flag is set to true
            if (gameObject.CompareTag("gridManager")) flag = true;

            //Initialize the tile, where will be inserted
            //the reference tile anchored by the solar panel
            Tile referenceTile = gameObject.AddComponent<Tile>();

            //Foreach game object with the script component "Tile",
            //if the tag is the same of the slot anchored by the panel,
            //the tile anchored will be this
            foreach (Tile tile in FindObjectsOfType<Tile>())
            {
                if(CompareTag(tile.gameObject.tag)) referenceTile = tile;
            }

            //For each child of tile, if it's present the house,
            //the item can't be positioned in that tile, so
            //it's not a valid drop and the flag is set to true
            foreach (Transform child in referenceTile.gameObject.transform) {
                if (child.gameObject.CompareTag("house")) flag = true;
            }

            //Else if it's a tile without a house or a solar panel,
            //the item can be positioned
            if(!flag)
            {
                //The item is dropped
                item.validDrop = true;

                //Anchor the item in the right position
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                    GetComponent<RectTransform>().anchoredPosition;
              
                //If the item in the slot is the solar panel
                //activate connections of tile where there's the panel
                if (item.gameObject.CompareTag("solarPanel"))
                {
                    ActivateConnections(referenceTile, item);
                }     
            }          
        }       
    }

    //Method to activate connections of tile where there's the panel
    private void ActivateConnections(Tile refTile, DragAndDropHandler item)
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
                //and if tile is the reference tile,
                //activate the vertical and horizontal connections
                //based on tile position. If there is a house in a tile,
                //next (or previous) connections are broken
                if (grid[j].Contains(refTile) && grid[j][i] == refTile)
                {
                    grid[j][i].IsActived(true, "verticalNode");
                    grid[j][i].IsActived(true, "horizontalNode");

                    switch (j)
                    {
                        case 0:
                            grid[j + 1][i].IsActived(true, "horizontalNode");
                            if (grid[j + 1][i].HasHouse()) break;

                            grid[j + 2][i].IsActived(true, "horizontalNode");
                            if (grid[j + 2][i].HasHouse()) break;

                            grid[j + 3][i].IsActived(true, "horizontalNode");
                            if (grid[j + 3][i].HasHouse()) break;

                            break;

                        case 1:

                            grid[j + 1][i].IsActived(true, "horizontalNode");
                            if (grid[j + 1][i].HasHouse()) break;

                            grid[j + 2][i].IsActived(true, "horizontalNode");
                            if (grid[j + 2][i].HasHouse()) break;

                            grid[j - 1][i].IsActived(true, "horizontalNode");

                            break;

                        case 2:

                            grid[j - 1][i].IsActived(true, "horizontalNode");
                            if (grid[j - 1][i].HasHouse()) break;

                            grid[j - 2][i].IsActived(true, "horizontalNode");

                            grid[j + 1][i].IsActived(true, "horizontalNode");

                            break;

                        case 3:

                            grid[j - 1][i].IsActived(true, "horizontalNode");
                            if (grid[j - 1][i].HasHouse()) break;

                            grid[j - 2][i].IsActived(true, "horizontalNode");
                            if (grid[j - 2][i].HasHouse()) break;

                            grid[j - 3][i].IsActived(true, "horizontalNode");

                            break;
                    }

                    switch (i)
                    {
                        case 0:
                            grid[j][i + 1].IsActived(true, "verticalNode");
                            if (grid[j][i + 1].HasHouse()) break;

                            grid[j][i + 2].IsActived(true, "verticalNode");
                            if (grid[j][i + 2].HasHouse()) break;

                            grid[j][i + 3].IsActived(true, "verticalNode");
                            if (grid[j][i + 3].HasHouse()) break;

                            break;

                        case 1:

                            grid[j][i + 1].IsActived(true, "verticalNode");
                            if (grid[j][i + 1].HasHouse()) break;

                            grid[j][i + 2].IsActived(true, "verticalNode");
                            if (grid[j][i + 2].HasHouse()) break;

                            grid[j][i - 1].IsActived(true, "verticalNode");

                            break;

                        case 2:

                            grid[j][i + 1].IsActived(true, "verticalNode");

                            grid[j][i - 1].IsActived(true, "verticalNode");
                            if (grid[j][i - 1].HasHouse()) break;

                            grid[j][i - 2].IsActived(true, "verticalNode");

                            break;

                        case 3:

                            grid[j][i - 1].IsActived(true, "verticalNode");
                            if (grid[j][i - 1].HasHouse()) break;

                            grid[j][i - 2].IsActived(true, "verticalNode");
                            if (grid[j][i - 2].HasHouse()) break;

                            grid[j][i - 3].IsActived(true, "verticalNode");

                            break;
                    }
                }
            }
        }

        //Assign the reference tile to the item dragged
        item.referenceTile = refTile;

    }
}
