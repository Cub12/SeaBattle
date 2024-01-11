using System.Collections.Generic;

namespace SeaBattle.Entity;

public class PlayerEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Rating { get; set; }
    public int NumWins { get; set; }
    public List<int> RatingHistory { get; } = [];
}