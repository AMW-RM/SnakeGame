using SnakeGame.Core.Interfaces;
using SnakeGame.Core.Models;
using System.Drawing;

namespace SnakeGame.Core.Game;

internal class GameState : IGameState
{
    public Snake Snake { get; private set; }
    public Position Food { get; private set; }
    public bool IsGameOver { get; private set; }
    public int GameBoardWidth { get; private set; }
    public int GameBoardHeight { get; private set; }

    public GameState(int widht, int height)
    {
        GameBoardWidth = widht;
        GameBoardHeight = height;

        InitializationGame();
    }
    public void Reset()
    {
        InitializationGame();
    }

    public void SetFood(Position food)
    {
        Food = food;
    }

    public void SetGameOver()
    {
        IsGameOver = true;
    }
        private void InitializationGame()
    {
        Snake = new Snake(new Position(GameBoardWidth / 2, GameBoardHeight / 2), Direction.Down);
        IsGameOver = false;

    }
    }
