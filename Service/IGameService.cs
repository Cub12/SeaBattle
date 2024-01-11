using SeaBattle.Entity;

namespace SeaBattle.Service;

public interface IGameService
{
    void AddPlayer(string name);  
    void PlayGames(int numGames, PlayerEntity player1, PlayerEntity player2);
    void ShowResults(PlayerEntity player1, PlayerEntity player2);
}