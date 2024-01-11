using System;
using System.Collections.Generic;
using SeaBattle.Entity;
using SeaBattle.Objects;
using SeaBattle.Repository.IRepository;

namespace SeaBattle.Service;

public class GameService(IPlayerRepository playerRepo, IGameRepository gameRepo) : IGameService
{
    public void AddPlayer(string name)
    {
        PlayerEntity player = new PlayerEntity { Name = name };
        playerRepo.AddPlayer(player);
    }

    public void PlayGames(int numGames, PlayerEntity player1, PlayerEntity player2)
    {
        for (int i = 0; i < numGames; i++)
        {
            bool isPlayer1Winner = PlayGame(player1, player2);
            UpdatePlayerStats(player1, isPlayer1Winner);
            UpdatePlayerStats(player2, !isPlayer1Winner);
            GameEntity dbGame = new GameEntity
            {
                PlayerId = player1.Id,
                IsWin = isPlayer1Winner
            };
            gameRepo.AddGame(dbGame);
        }
    }

    private bool PlayGame(PlayerEntity player1, PlayerEntity player2)
    {
        Player p1 = MapPlayer(player1);
        Player p2 = MapPlayer(player2);
        
        Game game = new Game(p1, p2);
        game.PlayToEnd();

        int p1FinalWins = p1.NumberOfWins;
        int p2FinalWins = p2.NumberOfWins;
        int p1WinsThisGame = p1FinalWins - player1.NumWins;
        int p2WinsThisGame = p2FinalWins - player2.NumWins;

        return p1WinsThisGame > p2WinsThisGame;
    }

    public void ShowResults(PlayerEntity player1, PlayerEntity player2)
    {
        Console.WriteLine();
        Console.WriteLine("Game History:");
        foreach (GameEntity game in gameRepo.GetAllGames())
        {
            int gameIndex = gameRepo.GetAllGames().IndexOf(game);
            string player1Result = game.IsWin ? "Win" : "Lose";
            string player2Result = game.IsWin ? "Lose" : "Win";

            Console.WriteLine("Game #" + game.Id);
            Console.WriteLine(player1.Name);
            Console.WriteLine("  Result: " + player1Result);
            Console.WriteLine("  Current Rating: " + player1.RatingHistory[gameIndex]);
            Console.WriteLine(player2.Name);
            Console.WriteLine("  Result: " + player2Result);
            Console.WriteLine("  Current Rating: " + player2.RatingHistory[gameIndex]);
            Console.WriteLine();
        }
        
        Console.WriteLine("Game Results:");
        List<PlayerEntity> players = playerRepo.GetAllPlayers();
        foreach (PlayerEntity player in players)
        {
            Console.WriteLine(player.Name);
            Console.WriteLine("Wins: " + player.NumWins);
            Console.WriteLine("Rating: " + player.Rating);
            Console.WriteLine();
        }
    }

    private Player MapPlayer(PlayerEntity entity)
    {
        return new Player(entity.Name)
        {
            NumberOfWins = entity.NumWins,
            Rating = entity.Rating
        };
    }

    private void UpdatePlayerStats(PlayerEntity player, bool isWin)
    {
        if (isWin)
        {
            player.NumWins++;
            player.Rating += 50;
        }

        player.RatingHistory.Add(player.Rating);
        playerRepo.UpdatePlayerStats(player);
    }
}