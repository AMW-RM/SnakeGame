using SnakeGame.Core.Models;

namespace SnakeGame.Core.Interfaces;

internal interface IFoodGenerator
{
    Position GenerateFood(IGameState gameState);
}
