using SeaBattle.Enums;

namespace SeaBattle.Objects.Ships;

public class Submarine : Ship
{
    public Submarine()
    {
        Name = "Submarine";
        Width = 1;
        OccupationType = OccupationType.Submarine;
    }
}