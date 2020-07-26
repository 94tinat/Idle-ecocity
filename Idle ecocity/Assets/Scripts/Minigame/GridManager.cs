using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //List of grid rows
    private List<Tile> rows;

    //List of rows lists
    public List<List<Tile>> grid;

    //List of tiles
    public List<Tile> tileList;

    private void Start()
    {
        //Initialize rows and grid
        rows = new List<Tile>();
        grid = new List<List<Tile>>();

        //Call creation grid method
        CreateGrid();
    }

    //Method to register each tile to list
    public void AddTileToList(Tile tile)
    {
        if (tile == null)
        {
            tileList = new List<Tile>();
        }

        tileList.Add(tile);

    }

    //Method to create the grid
    private void CreateGrid()
    {
        //Variable to identify the row to insert in grid
        int id = 0;

        //For each row in grid
        for (int j = 0; j < 4; j++)
        {
            //Clear the list of rows to insert new ones
            rows = new List<Tile>();

            //For each tile in a row
            for (int i = 0; i < 4; i++)
            {
                //Switch-case to recognize a specific row of the grid:
                //If it's the first position, insert the first tile of each row in rows list
                //Otherwise insert the tile in "id+4" position and increment the id
                switch(j)
                {
                    case 0:
                        if (i == 0)
                            rows.Add(tileList[0]);
                        else
                        {
                            rows.Add(tileList[id + 4]);
                            id += 4;
                        }
                        
                        break;

                    case 1:
                        if (i == 0)
                        {
                            rows.Add(tileList[1]);
                            id = 1;
                        }
                           
                        else
                        {
                            rows.Add(tileList[id + 4]);
                            id += 4;
                        }
                        break;

                    case 2:
                        if (i == 0)
                        {
                            rows.Add(tileList[2]);
                            id = 2;
                        }
                        else
                        {
                            rows.Add(tileList[id + 4]);
                            id += 4;
                        }
                        break;

                    case 3:
                        if (i == 0)
                        { 
                            rows.Add(tileList[3]);
                            id = 3;
                        }
                        else
                        {
                            rows.Add(tileList[id + 4]);
                            id += 4;
                        }
                        break;
                }
               
            }
            //Add the list of rows with 4 tiles in grid
            grid.Add(rows);

        }

    }

}
