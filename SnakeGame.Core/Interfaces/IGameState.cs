using SnakeGame.Core.Models;

namespace SnakeGame.Core.Interfaces;

internal interface IGameState
{
    Snake Snake { get; }
    Position Food { get; }
    bool IsGameOver { get; }
    int GameBoardWidth { get; } 
    int GameBoardHeight { get; }

}
