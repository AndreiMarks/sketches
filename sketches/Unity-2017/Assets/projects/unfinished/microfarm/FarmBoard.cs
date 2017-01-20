using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FarmBoard : MicroFarmBehaviour
{
    public Bones.Grid grid;
    public int width;
    public int height;

    public FarmBoardSquare squarePrefab;

    public Color groundColorOne;
    public Color groundColorTwo;
    
    private List<FarmBoardSquare> _squares = new List<FarmBoardSquare>();
    
    public void CreateBoard()
    {
        Debug.Log("Creating board.");
        grid.SetGridSize(width, height);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 spawnPos = grid.GetGridPosition(x, y);
                FarmBoardSquare newSquare = transform.InstantiateChild(squarePrefab, spawnPos);
                _squares.Add(newSquare);

                newSquare.SetColor( _Colors.GetRandomColorByGradientType( ThemeColor.GreenGround ) );
            }
        }
    }
}
