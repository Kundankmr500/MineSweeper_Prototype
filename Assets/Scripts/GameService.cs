using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameService : MonoBehaviour
{
    public int RowSize;
    public int ColSize;
    public int MineCount;
    public Transform ObjParent;
    public Tile[] AllPrefabs;

    private Tile[,] Grid;


    void Start()
    {
        Grid = new Tile[RowSize, ColSize];

        for (int i = 0; i < MineCount; i++)
        {
            PlaceMine();
        }
        PlaceClues();
    }


    private void PlaceMine()
    {
        int XIndex = Random.Range(0, RowSize);
        int YIndex = Random.Range(0, ColSize);

        if(Grid[XIndex,YIndex] == null)
        {
            Tile mineTile = Instantiate(AllPrefabs[0], 
                                  new Vector3(XIndex, YIndex, 0), Quaternion.identity, ObjParent) as Tile;

            Grid[XIndex, YIndex] = mineTile;

            //Debug.Log("( " + XIndex + " , " + YIndex + " )");
        }
        else
        {
            PlaceMine();
        }
    }

    private bool isInvalid( int row, int column)
    {
        return (row < 0 || row > RowSize - 1 || column < 0 ||  column > ColSize - 1);
    }

    private void PlaceClues()
    {
        for (int x = 0; x < RowSize; x++)
        {
            for (int y = 0; y < ColSize; y++)
            {
                if (Grid[x, y] == null)
                {
                    int numMines = CheckForNumMines(x, y);


                    //// Up
                    //if(y + 1 < ColSize && Grid[x , y + 1] != null && Grid[x, y + 1].TileType == TileType.Mine)
                    //{
                    //    numMines++;
                    //}
                    //// forward
                    //if (x + 1 < RowSize && Grid[x + 1, y] != null && Grid[x + 1, y].TileType == TileType.Mine)
                    //{
                    //    numMines++;
                    //}
                    //// Down
                    //if (y - 1 >= 0 && Grid[x, y - 1] != null && Grid[x, y - 1].TileType == TileType.Mine)
                    //{
                    //    numMines++;
                    //}
                    ////backward
                    //if (x - 1 >= 0 && Grid[x - 1, y] != null && Grid[x - 1, y].TileType == TileType.Mine)
                    //{
                    //    numMines++;
                    //}
                    ////up_Forward
                    //if (x + 1 < RowSize && y + 1 < ColSize && Grid[x + 1, y + 1] != null 
                    //                    && Grid[x + 1, y + 1].TileType == TileType.Mine)
                    //{
                    //    numMines++;
                    //}
                    ////Up_Backward
                    //if (x - 1 >= 0 && y + 1 < ColSize && Grid[x - 1, y + 1] != null 
                    //                && Grid[x - 1, y + 1].TileType == TileType.Mine)
                    //{
                    //    numMines++;
                    //}
                    ////Down_Forward
                    //if (x + 1 < RowSize && y - 1 >= 0 && Grid[x + 1, y - 1] != null 
                    //                && Grid[x + 1, y - 1].TileType == TileType.Mine)
                    //{
                    //    numMines++;
                    //}
                    ////Down_Forward
                    //if (x - 1 >= 0 && y - 1 >= 0 && Grid[x - 1, y - 1] != null 
                    //           && Grid[x - 1, y - 1].TileType == TileType.Mine)
                    //{
                    //    numMines++;
                    //}

                    if (numMines > 0)
                    {
                        Tile clueTile = Instantiate(AllPrefabs[numMines], new Vector3(x, y, 0), Quaternion.identity, ObjParent);
                        Grid[x, y] = clueTile;
                    }
                    else
                    {
                        PlaceBlanks(x, y);
                    }
                }

            }
        }
    }

    private int CheckForNumMines(int x, int y)
    {
        int numMines = 0;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (!isInvalid(i, j) && Grid[i, j] != null && Grid[i, j].TileType == TileType.Mine)
                {
                    numMines++;
                }
            }
        }

        return numMines;
    }

    private void PlaceBlanks(int xIndex, int yIndex)
    {
        Tile blankTile = Instantiate(AllPrefabs[9], new Vector3(xIndex, yIndex, 0), Quaternion.identity, ObjParent);
        Grid[xIndex, yIndex] = blankTile;
    }

    private void Update()
    {
        CheckUserInput();
    }


    private void CheckUserInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int xPos = Mathf.RoundToInt(mousePos.x);
            int yPos = Mathf.RoundToInt(mousePos.y);


            if ((xPos >= 0 && xPos < RowSize) && (yPos >= 0 && xPos < ColSize))
            {
                Tile tile = Grid[xPos, yPos];
                tile.RevealedTile();
            }
        }
    }
}
