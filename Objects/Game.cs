using System;
using SeaBattle.Enums;
using SeaBattle.Objects.Boards;

namespace SeaBattle.Objects;

public class Game
{
    private Player Player1 { get; }
    private Player Player2 { get; }

    public Game(Player player1, Player player2)
    {
        Player1 = player1;
        Player2 = player2;

        Player1.PlaceShips();
        Player2.PlaceShips();

        Player1.OutputBoards();
        Player2.OutputBoards();
    }

    private void PlayRound(Player attacker, Player defender)
    {
        // Player 1 fires a shot, then Player 2 fires a shot.
        Coordinates coordinates = attacker.FireShot();
        ShotResult result = defender.ProcessShot(coordinates);
        attacker.ProcessShotResult(coordinates, result);
    }

    public void PlayToEnd()
    {
        while (!Player1.HasLost && !Player2.HasLost)
        {
            PlayRound(Player1, Player2);
            if (Player2.HasLost) break; // If player 2 already lost, don't let them take another turn.
            PlayRound(Player2, Player1);
        }

        Player1.OutputBoards();
        Player2.OutputBoards();

        if (Player1.HasLost)
        {
            Console.WriteLine(Player2.Name + " has won the game!");
            Console.WriteLine();
            Console.WriteLine();
            Player2.NumberOfWins++;
            Player2.Rating += 50;
        }
        else if (Player2.HasLost)
        {
            Console.WriteLine(Player1.Name + " has won the game!");
            Console.WriteLine();
            Console.WriteLine();
            Player1.NumberOfWins++;
            Player1.Rating += 50;
        }
    }
}