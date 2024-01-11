using System.ComponentModel;

namespace SeaBattle.Enums;

public enum OccupationType
{
    [Description("o")] Empty,
    [Description("X")] Hit,
    [Description("M")] Miss,
    [Description("A")] AircraftCarrier,
    [Description("B")] Battleship,
    [Description("C")] Cruiser,
    [Description("D")] Destroyer,
    [Description("S")] Submarine
}