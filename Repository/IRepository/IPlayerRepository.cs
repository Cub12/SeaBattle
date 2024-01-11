using System.Collections.Generic;
using SeaBattle.Entity;

namespace SeaBattle.Repository.IRepository;

public interface IPlayerRepository
{
    void AddPlayer(PlayerEntity player);
    PlayerEntity GetPlayer(int playerId);
    List<PlayerEntity> GetAllPlayers();
    void UpdatePlayerStats(PlayerEntity player);
}