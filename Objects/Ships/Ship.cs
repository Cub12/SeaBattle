using SeaBattle.Enums;

namespace SeaBattle.Objects.Ships;

public abstract class Ship
{
    public string Name { get; protected set; }
    public int Width { get; protected set; }
    public int Hits { get; set; }
    public OccupationType OccupationType { get; protected set; }
    public bool IsSunk => Hits >= Width;
}