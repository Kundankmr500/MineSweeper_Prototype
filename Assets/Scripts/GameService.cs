using System.Collections.Generic;
using UnityEngine;


public class GameService : MonoBehaviour
{
    [Header("User Varibles")]
    public bool Uncover;
    public int RowSize;
    public int ColSize;
    public int MineCount;
    public GameObject GameOverScreen;
    public Transform ObjParent;
    public Tile[] AllPrefabs;

    private List<Tile> activeMines;
    private Tile[,] Grid;


    void Start()
    {
        activeMines = new List<Tile>();
        Grid = new Tile[RowSize, ColSize];

        for (int i = 0; i < MineCount; i++)
        {
            PlaceMine();
        }
        PlaceClues();
    }


    // Placing all the mine tiles on the board
    private void PlaceMine()
    {
        int XIndex = Random.Range(0, RowSize);
        int YIndex = Random.Range(0, ColSize);

        if(Grid[XIndex,YIndex] == null)
        {
            Tile mineTile = Instantiate(AllPrefabs[0], 
                                  new Vector3(XIndex, YIndex, 0), Quaternion.identity, ObjParent) as Tile;

            Grid[XIndex, YIndex] = mineTile;
            activeMines.Add(mineTile);
        }
        else
        {
            PlaceMine();
        }
    }


    // Checking if the tile index in the range of board array
    private bool isInvalid( int row, int column)
    {
        return (row < 0 || row > RowSize - 1 || column < 0 ||  column > ColSize - 1);
    }


    // Placing all clues on the board
    private void PlaceClues()
    {
        for (int x = 0; x < RowSize; x++)
        {
            for (int y = 0; y < ColSize; y++)
            {
                if (Grid[x, y] == null)
                {
                    // Getting Nummines int the variable
                    int numMines = GetNumMines(x, y);

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


    private int GetNumMines(int x, int y)
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


    //Placing blank tiles on the board
    private void PlaceBlanks(int xIndex, int yIndex)
    {
        Tile blankTile = Instantiate(AllPrefabs[9], new Vector3(xIndex, yIndex, 0), Quaternion.identity, ObjParent);
        Grid[xIndex, yIndex] = blankTile;
    }


    public void MouseRightClickProcess(int xPos, int yPos)
    {
        if (!isInvalid(xPos, yPos))
        {
            Tile tile = Grid[xPos, yPos];
            tile.ToggleFlagTileState();
        }
    }


    public void MouseLeftClickProcess(int xPos, int yPos)
    {
        // Clicking only in game board condition
        if (!isInvalid(xPos, yPos))
        {
            Tile tile = Grid[xPos, yPos];

            if (tile.IsCovered && tile.TileState == TileState.Normal)
            {
                // Game Over condition
                if (tile.TileType == TileType.Mine)
                {
                    GameOver();
                }
                // Revealing neighbour tile condition
                else if (tile.TileType == TileType.Blank)
                {
                    RevealAdjacentTilesUsingDFS(xPos, yPos);
                }
                // Revealing single clicked tile
                else
                {
                    tile.RevealedTile();
                }
            }
        }
    }


    // Perform Depth First Search to Reveal Adjacent Tiles
    private void RevealAdjacentTilesUsingDFS(int row, int column)
    {
        if (isInvalid(row, column) || Grid[row, column].IsVisited == true 
                        || Grid[row, column].TileState == TileState.Flagged) // if invalid row, column or already visited
        {
            return;
        }

        Tile tile = Grid[row, column];
        tile.IsVisited = true;

        if (tile.TileType == TileType.Clue) // check for exit condition to stop checking further(if we found the clue tile)
        {
            tile.RevealedTile();
            return;
        }

        tile.RevealedTile();

        // Checking for all possible side neighbour
        RevealAdjacentTilesUsingDFS(row, column - 1);
        RevealAdjacentTilesUsingDFS(row - 1, column);
        RevealAdjacentTilesUsingDFS(row, column + 1);
        RevealAdjacentTilesUsingDFS(row + 1, column);
        RevealAdjacentTilesUsingDFS(row + 1, column + 1);
        RevealAdjacentTilesUsingDFS(row - 1, column + 1);
        RevealAdjacentTilesUsingDFS(row + 1, column - 1);
        RevealAdjacentTilesUsingDFS(row - 1, column - 1);
    }


    private void GameWin()
    {

    }

    // GameOver implementation
    private void GameOver()
    {
        for (int i = 0; i < activeMines.Count; i++)
        {
            if (activeMines[i].TileState == TileState.Normal)
                activeMines[i].RevealedTile();
        }
        GameOverScreen.SetActive(true);
    }


    public void UncoverAllTileToggle()
    {
        if(!Uncover)
        {
            for (int x = 0; x < RowSize; x++)
            {
                for (int y = 0; y < ColSize; y++)
                {
                    Tile tile = Grid[x, y];
                    tile.RevealedTile();
                }
            }
            Uncover = true;
        }
        else
        {
            for (int x = 0; x < RowSize; x++)
            {
                for (int y = 0; y < ColSize; y++)
                {
                    Tile tile = Grid[x, y];
                    tile.CoveredTile();
                }
            }
            Uncover = false;
        }

    }
}
