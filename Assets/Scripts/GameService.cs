using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    public int RowSize;
    public int ColSize;
    public int MineCount;

    private GameObject[,] Grid;


    void Start()
    {
        Grid = new GameObject[RowSize, ColSize];

        for (int i = 0; i < MineCount; i++)
        {
            PlaceMine();
        }

    }


    private void PlaceMine()
    {
        int XIndex = Random.Range(0, RowSize);
        int YIndex = Random.Range(0, ColSize);

        if(Grid[XIndex,YIndex] == null)
        {
            GameObject empty = new GameObject();
            Grid[XIndex, YIndex] = empty;

            Debug.Log("( " + XIndex + " , " + YIndex + " )");
        }
        else
        {
            PlaceMine();
        }
    }
}
