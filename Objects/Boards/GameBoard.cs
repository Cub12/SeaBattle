using System.Collections.Generic;

namespace SeaBattle.Objects.Boards;

public class GameBoard
{
    public List<Cell> Cells { get; }
    public GameBoard()
    {
        Cells = [];
        for (int i = 1; i <= 10; i++)
        {
            for (int j = 1; j <= 10; j++)
            {
                Cells.Add(new Cell(i, j));
            }
        }
    }
}