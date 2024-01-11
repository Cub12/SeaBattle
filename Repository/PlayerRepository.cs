using System;
using System.Collections.Generic;
using System.Linq;
using SeaBattle.Entity;
using SeaBattle.Repository.IRepository;

namespace SeaBattle.Repository;

public class PlayerRepository(List<PlayerEntity> players) : IPlayerRepository
{
    public void AddPlayer(PlayerEntity player)
    {
        if (players.Count > 0 && PlayerNameExists(player.Name))
        {
            Console.WriteLine("Name already used. Try something else");
            return;
        }
        
        player.Id = players.Count + 1;
        players.Add(player);
    }

    private bool PlayerNameExists(string name) { return GetPlayerIdByName(name) != -1; }
    
    public bool GetValidPlayerNameInput(string name, PlayerRepository playerRepo)
    {
        if (playerRepo.PlayerNameExists(name)) return true;
        Console.WriteLine("Name not found in DB. Try again.");
        return false;
    }

    public PlayerEntity GetPlayer(int playerId)
    {
        return players.FirstOrDefault(p => p.Id == playerId);
    }

    public int GetPlayerIdByName(string name)
    {
        return players.FirstOrDefault(x => x.Name == name)?.Id ?? -1;
    }

    public List<PlayerEntity> GetAllPlayers() { return players; }

    public void UpdatePlayerStats(PlayerEntity player)
    {
        PlayerEntity dbPlayer = GetPlayer(player.Id);
        if (dbPlayer == null) return;
        dbPlayer.NumWins = player.NumWins;
        dbPlayer.Rating = player.Rating;
    }
}