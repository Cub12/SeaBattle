using SeaBattle.Enums;

namespace SeaBattle.Objects.Ships;

public class Battleship : Ship
{
    public Battleship()
    {
        Name = "Battleship";
        Width = 4;
        OccupationType = OccupationType.Battleship;
    }
}