using SeaBattle.Enums;

namespace SeaBattle.Objects.Ships;

public class Cruiser : Ship
{
    public Cruiser()
    {
        Name = "Cruiser";
        Width = 3;
        OccupationType = OccupationType.Cruiser;
    }
}