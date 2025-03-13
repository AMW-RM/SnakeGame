using SnakeGame.Core.Interfaces;
using SnakeGame.Core.Models;

namespace SnakeGame.Core.Game;

public class GameEngine
{
    private readonly IGameState _gameState;
    private readonly IGameReander _gameReander;
    private readonly IFoodGenerator _foodGenerator;
    private readonly IGameLoop _gameLoop;

    public GameEngine(IGameState gameState,
        IGameReander gameReander,
        IFoodGenerator foodGenerator,
        IGameLoop gameLoop)
    {
        _gameState = gameState;
        _gameReander = gameReander;
        _foodGenerator = foodGenerator;
        _gameLoop = gameLoop;
    }

    public void Update()
    {
        if (_gameState.IsGameOver)
        {
            _gameLoop.Stop();
            _gameState.Reset();
            return;
        }
        var newHead = CalculateNewPosition(_gameState.Snake.Head, _gameState.Snake.CurrentDirection);

        if (_gameState.Snake.CollidesWith(newHead))
        {
            _gameState.SetGameOver();
            return;
        }

        _gameState.Snake.Move(newHead);
        if (newHead.Equals(_gameState.Food))
        {
            GenerateNewFood();
        }
        else
        {
            _gameState.Snake.RemoveTail();
        }

        _gameReander.Reander(_gameState);
    }

    private void GenerateNewFood()
    {
        var newFood = _foodGenerator.GenerateFood(_gameState);
        _gameState.SetFood(newFood);
    }

    private Position CalculateNewPosition(Position head, Direction currentDirection)
    {
        var newX = (head.X + currentDirection.DeltaX + _gameState.GameBoardWidth) % _gameState.GameBoardWidth;
        var newY = (head.Y + currentDirection.DeltaY + _gameState.GameBoardHeight) % _gameState.GameBoardHeight;
        return new Position(newX, newY);
    }


}
