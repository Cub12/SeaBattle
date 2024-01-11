using System.Collections.Generic;
using SeaBattle.Entity;

namespace SeaBattle.Repository.IRepository;

public interface IGameRepository
{
    void AddGame(GameEntity game);
    List<GameEntity> GetGamesForPlayer(int playerId); 
    List<GameEntity> GetAllGames();
}