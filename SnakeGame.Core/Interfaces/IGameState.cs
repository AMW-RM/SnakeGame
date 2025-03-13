using SnakeGame.Core.Models;

namespace SnakeGame.Core.Interfaces;

public interface IGameState
{
    Snake Snake { get; }
    Position Food { get; }
    bool IsGameOver { get; }
    int GameBoardWidth { get; } 
    int GameBoardHeight { get; }
    void Reset();
    void SetGameOver();
    void SetFood(Position food);

}
