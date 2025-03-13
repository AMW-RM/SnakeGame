using SnakeGame.Core.Interfaces;
using SnakeGame.Core.Models;

namespace SnakeGame.Core.Game;

public class GameState : IGameState
{
    public Snake Snake { get; private set; }
    public Position Food { get; private set; }
    public bool IsGameOver { get; private set; }
    public int GameBoardWidth { get; }
    public int GameBoardHeight { get; }

       public GameState(int width, int height)
    {
        GameBoardWidth = width;
        GameBoardHeight = height;
        InitializeGame();
    }

    public void Reset()
    {
        InitializeGame();
    }

    public void SetFood(Position food)
    {
        Food = food;
    }

    public void SetGameOver()
    {
        IsGameOver = true;
    }

    private void InitializeGame()
    {
        Snake = new Snake(new Position(GameBoardWidth / 2, GameBoardHeight / 2),
            Direction.Right
        );
        IsGameOver = false;
    }
}