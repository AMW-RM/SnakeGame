
using SnakeGame.Core.Interfaces;
using SnakeGame.Core.Models;

namespace SnakeGame.Core.Game;
public class GameEngine
{
    private readonly GameState _gameState;
    private readonly IGameReander _gameReander;
    private readonly IFoodGenerator _foodGenerator;
    private readonly IGameLoop _gameLoop;

    public GameEngine(
        GameState gameState,
        IGameReander gameReander,
        IFoodGenerator foodGenerator,
        IGameLoop gameLoop)
    {
        _gameState = gameState;
        _gameReander = gameReander;
        _foodGenerator = foodGenerator;
        _gameLoop = gameLoop;
    }
    internal void Update()
    {
        if (_gameState.IsGameOver)
        {
            _gameLoop.Stop();
            _gameState.Reset();
            return;
        }
        var NewHead = CalculateNewPosition(_gameState.Snake.Head, _gameState.Snake.CurrentDirection);
        // czy nie nowa głowa nie koliduje z ciałem wężem
        //przesunąć węża na nową pozycje.
    }

    //nowa metoda
    public void RestartGame()
    {
        //co ustawić
    }

    public void ChangeDirection(Direction newDirection)
    {
        //co ustawic
        
    }

    private Position CalculateNewPosition(Position head, Direction currentDirection)
    {
        var newX = (head.X + currentDirection.DeltaX + _gameState.GameBoardWidth) % _gameState.GameBoardWidth;
        var newY = (head.Y + currentDirection.DeltaY + _gameState.GameBoardHeight) % _gameState.GameBoardHeight;
        return new Position(newX, newY);

    }
}