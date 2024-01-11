namespace SeaBattle;

internal abstract class Program
{
    private static void Main()
    {
        SeaBattleGame game = new SeaBattleGame();  
        game.Run();
    }
}