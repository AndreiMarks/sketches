using System.Collections.Generic;
using UnityEngine;

public class ConwayCubesDisplay : MonoBehaviour, IGameOfLifeDisplay
{
    [SerializeField] private ConwayCube _cubePrefab;
    [SerializeField] private Transform _cubeHolder;

    private List<ConwayCube> _conwayCubes = new List<ConwayCube>();
    
    public Bounds GridBounds { get; private set; }
    
    public void SetUpDisplay(int columns, int rows, List<bool> grid)
    {
        for (int i = 0; i < grid.Count; i++)
        {
            int column = i % columns;
            int row = Mathf.FloorToInt((float)i/columns);
            Vector3 position = Vector3.right * column + Vector3.forward * row;
            ConwayCube newCube = Instantiate(_cubePrefab);
            newCube.transform.position = position;
            newCube.transform.SetParent(_cubeHolder);

            newCube.SetOccupied(grid[i]);
            _conwayCubes.Add(newCube);
        }
        
        Bounds gridBounds = new Bounds(_conwayCubes[0].transform.position, Vector3.zero);
        gridBounds.Encapsulate(_conwayCubes[_conwayCubes.Count - 1].transform.position);
        GridBounds = gridBounds;
    }

    public void UpdateDisplay(List<bool> grid)
    {
        for (int i = 0; i < grid.Count; i++)
        {
            _conwayCubes[i].SetOccupied(grid[i]);
        }
    }
}
