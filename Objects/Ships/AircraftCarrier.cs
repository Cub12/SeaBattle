using SeaBattle.Enums;

namespace SeaBattle.Objects.Ships;

public class AircraftCarrier : Ship
{
    public AircraftCarrier()
    {
        Name = "Aircraft Carrier";
        Width = 5;
        OccupationType = OccupationType.AircraftCarrier;
    }
}