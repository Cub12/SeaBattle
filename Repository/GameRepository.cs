using System.Collections.Generic;
using System.Linq;
using SeaBattle.Entity;
using SeaBattle.Repository.IRepository;

namespace SeaBattle.Repository;

public class GameRepository(List<GameEntity> games) : IGameRepository
{
    public void AddGame(GameEntity game)
    {
        game.Id = games.Count + 1;
        games.Add(game);    
    }
    
    public List<GameEntity> GetGamesForPlayer(int playerId)
    {
        return games.Where(g => g.PlayerId == playerId).ToList();
    }

    public List<GameEntity> GetAllGames() { return games; }
}