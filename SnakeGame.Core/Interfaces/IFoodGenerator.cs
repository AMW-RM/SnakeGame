using SnakeGame.Core.Models;

namespace SnakeGame.Core.Interfaces;

public interface IFoodGenerator
{
    Position GenerateFood(IGameState gameState);
}
