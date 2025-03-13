using SnakeGame.Core.Interfaces;
using SnakeGame.Core.Models;

namespace SnakeGame.Core.Game;

internal class RandomFoodGenerator : IFoodGenerator
{
    private readonly Random _random = new Random();
    //private readonly Random _random = new ();

    public Position GenerateFood(Interfaces.IGameState gameState)
    {
        Position food;

        do
        {
            food = new Position(_random.Next(gameState.GameBoardWidth), _random.Next(gameState.GameBoardHeight));

        } while (gameState.Snake.CollidesWith(food));

        return food;

    }
}
