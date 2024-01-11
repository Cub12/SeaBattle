using SeaBattle.Enums;

namespace SeaBattle.Objects.Ships;

public class Destroyer : Ship
{
    public Destroyer()
    {
        Name = "Destroyer";
        Width = 2;
        OccupationType = OccupationType.Destroyer;
    }
}