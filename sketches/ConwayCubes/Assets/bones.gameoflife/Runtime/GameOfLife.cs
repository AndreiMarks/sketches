using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bones.GameOfLife
{
    public class GameOfLife
    {
        private int _columns;
        private int _rows;
        
        private List<bool> _grid = new List<bool>();
        private List<bool> _nextGrid = new List<bool>();

        private List<bool> _currentGrid;

        private IGameOfLifeDisplay _display;
        
        public GameOfLife(int columns, int rows, IGameOfLifeDisplay display)
        {
            _columns = columns;
            _rows = rows;
            
            for (int i = 0; i < _columns * _rows; i++)
            {
                bool isOccupied = UnityEngine.Random.value < 0.1f;
                _grid.Add(isOccupied);
                _nextGrid.Add(isOccupied);
            }

            _currentGrid = _grid;

            _display = display;
            _display.SetUpDisplay(_columns, _rows, _currentGrid);
        }

        public void Update()
        {
            ProcessGrid();
            _display.UpdateDisplay(_currentGrid);
        }

        private void ProcessGrid()
        {
            for (int i = 0; i < _currentGrid.Count; i++)
            {
                bool makeAlive = EvaluateCell(i, _currentGrid, _columns);
                _nextGrid[i] = makeAlive;
            }

            _currentGrid = _nextGrid;
        }

        private bool EvaluateCell(int index, 
                                  List<bool> currentGrid, 
                                  int columns)
        {
            bool isAlive = currentGrid[index];
            int neighborCount =
                GetLiveNeighborsCount(index, currentGrid, columns);

            return (isAlive && neighborCount == 2) ||
                   (isAlive && neighborCount == 3) ||
                   (!isAlive && neighborCount == 3);
        }

        private int GetLiveNeighborsCount(int index, 
                                          List<bool> currentGrid,
                                          int columns) 
        {
            bool isFirstColumn = index % columns == 0;
            bool isLastColumn = index % columns == columns - 1;
            
            int[] indicesToCheck = new int[]
                                   {
                                       isFirstColumn ? -1 : index + columns - 1, // NW 
                                       isFirstColumn ? -1 : index - 1,           // W
                                       isFirstColumn ? -1 : index - columns - 1, // SW
                                       index + columns,     // N 
                                       index - columns,     // S
                                       isLastColumn ? -1 : index + columns + 1, // NE
                                       isLastColumn ? -1 : index + 1,           // E
                                       isLastColumn ? -1 : index - columns + 1  // SE
                                   };

            int aliveCount = 0;
            for (int i = 0; i < indicesToCheck.Length; i++)
            {
                int indexToCheck = indicesToCheck[i];
                if (indexToCheck < 0 ||
                    indexToCheck >= currentGrid.Count)
                {
                    continue;
                }

                aliveCount += currentGrid[indexToCheck] ? 1 : 0;
            }

            return aliveCount;
        }
    }
}
