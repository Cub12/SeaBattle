using System;
using SeaBattle.Entity;
using SeaBattle.Repository;
using SeaBattle.Service;

namespace SeaBattle;

public class SeaBattleGame
{
    public void Run()
    {
        DbContext db = new DbContext();
        PlayerRepository playerRepo = new PlayerRepository(db.Players);
        GameRepository gameRepo = new GameRepository(db.Games);
        GameService gameService = new GameService(playerRepo, gameRepo);

        while (true)
        {
            Console.Write("Enter name (or 'q' to quit): ");
            string name = Console.ReadLine();
            PlayerEntity player = new PlayerEntity { Name = name };
            
            if (name is "q" && playerRepo.GetAllPlayers().Count >= 2) { break; }
            if (name != "q") {
                playerRepo.AddPlayer(player);
            }
            else
            {
                Console.WriteLine("Fewer than two names entered.");
            }
        }

        Console.WriteLine();
        
        PlayerEntity player1 = GetPlayerEntityFromUser(playerRepo);
        PlayerEntity player2 = GetPlayerEntityFromUser(playerRepo);

        Console.WriteLine();
        Console.WriteLine("How many games?");
        int numGames = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

        gameService.PlayGames(numGames, player1, player2);
        gameService.ShowResults(player1, player2);
    }

    private PlayerEntity GetPlayerEntityFromUser(PlayerRepository playerRepo)
    {
        string playerName = null;
        bool playerNameExist = false;
        
        while (!playerNameExist)
        {
            Console.Write("Enter name for player: ");
            playerName = Console.ReadLine();
            playerNameExist = playerRepo.GetValidPlayerNameInput(playerName, playerRepo);
        }

        int playerId = playerRepo.GetPlayerIdByName(playerName);
        PlayerEntity player = playerRepo.GetPlayer(playerId);

        return player;
    }
}