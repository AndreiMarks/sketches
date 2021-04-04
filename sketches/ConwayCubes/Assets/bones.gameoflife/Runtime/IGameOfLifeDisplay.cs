using System.Collections.Generic;

public interface IGameOfLifeDisplay
{
    void SetUpDisplay(int columns, int rows, List<bool> grid);
    void UpdateDisplay(List<bool> grid);
}
