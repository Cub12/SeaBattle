using SeaBattle.Extensions;
using System.Collections.Generic;
using System.Linq;
using SeaBattle.Enums;

namespace SeaBattle.Objects.Boards;

public class FiringBoard : GameBoard
{
    public List<Coordinates> GetOpenRandomCells()
    {
        return Cells.Where(x => x.OccupationType == OccupationType.Empty && x.IsRandomAvailable)
            .Select(x => x.Coordinates).ToList();
    }

    public List<Coordinates> GetHitNeighbors()
    {
        List<Cell> cells = [];
        IEnumerable<Cell> hits = Cells.Where(x => x.OccupationType == OccupationType.Hit);
        foreach (Cell hit in hits)
        {
            cells.AddRange(GetNeighbors(hit.Coordinates).ToList());
        }

        return cells.Distinct().Where(x => x.OccupationType == OccupationType.Empty)
            .Select(x => x.Coordinates).ToList();
    }

    private List<Cell> GetNeighbors(Coordinates coordinates)
    {
        int row = coordinates.Row;
        int column = coordinates.Column;
        List<Cell> cells = [];
        
        if (column > 1)
        {
            cells.Add(Cells.At(row, column - 1));
        }
        if (row > 1)
        {
            cells.Add(Cells.At(row - 1, column));
        }
        if (row < 10)
        {
            cells.Add(Cells.At(row + 1, column));
        }
        if (column < 10)
        {
            cells.Add(Cells.At(row, column + 1));
        }

        return cells;
    }
}