using System.Collections.Generic;
using System.Linq;
using SeaBattle.Objects.Boards;

namespace SeaBattle.Extensions;

public static class CellExtensions
{
    public static Cell At(this IEnumerable<Cell> cells, int row, int column)
    {
        return cells.First(x => x.Coordinates.Row == row && x.Coordinates.Column == column);
    }

    public static List<Cell> Range(this IEnumerable<Cell> cells, int startRow, int startColumn, int endRow, int endColumn)
    {
        return cells.Where(x => x.Coordinates.Row >= startRow
                                 && x.Coordinates.Column >= startColumn
                                 && x.Coordinates.Row <= endRow
                                 && x.Coordinates.Column <= endColumn).ToList();
    }
}