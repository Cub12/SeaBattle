using SeaBattle.Extensions;
using System.ComponentModel;
using SeaBattle.Enums;

namespace SeaBattle.Objects.Boards;

public class Cell(int row, int column)
{
    public OccupationType OccupationType { get; set; } = OccupationType.Empty;
    public Coordinates Coordinates { get; } = new(row, column);

    public string Status => OccupationType.GetAttributeOfType<DescriptionAttribute>().Description;

    public bool IsOccupied =>
        OccupationType is OccupationType.Battleship or OccupationType.Destroyer or OccupationType.Cruiser 
            or OccupationType.Submarine or OccupationType.AircraftCarrier;

    public bool IsRandomAvailable =>
        (Coordinates.Row % 2 == 0 && Coordinates.Column % 2 == 0)
        || (Coordinates.Row % 2 == 1 && Coordinates.Column % 2 == 1);
}