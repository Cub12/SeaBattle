using System.Collections.Generic;

namespace SeaBattle.Entity;

public class DbContext
{
    public List<PlayerEntity> Players { get; } = [];
    public List<GameEntity> Games { get; } = [];
}