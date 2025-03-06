using SnakeGame.Core.Game;
using SnakeGame.Core.Interfaces;
using SnakeGame.Core.Models;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeGame.Reanders;

internal class WpfGameReander : IGameReander
{
    private readonly Canvas _gameBoard;
    private readonly int _squareSize;

    public WpfGameReander(Canvas gameBoard, int squareSize, int widht, int height)
    {
        _gameBoard = gameBoard;
        _squareSize = squareSize;
        _gameBoard.Width = widht * squareSize;
        _gameBoard.Height = height * squareSize;
    }
    public void Clear()
    {
        _gameBoard.Children.Clear();
    }

    public void Reander(IGameState gameState)
    {
        Clear();
        ReanderSnake(gameState.Snake);
        ReanderFood(gameState.Food);
    }

    private void ReanderFood(Position food)
    {
        var foodshape = new Ellipse
        {
            Width = _squareSize,
            Height = _squareSize,
            Fill = Brushes.Yellow
        };
        Canvas.SetLeft(foodshape, food.X * _squareSize);
        Canvas.SetTop(foodshape, food.Y * _squareSize);
        _gameBoard.Children.Add(foodshape);
    }

    private void ReanderSnake(Snake snake)
    {
        foreach (var part in snake.Parts)
        {
            var rectangle = new Rectangle
            {
                Width = _squareSize - 1,
                Height = _squareSize - 1,
                Fill = Brushes.White
            };
            Canvas.SetLeft(rectangle, part.X * _squareSize);
            Canvas.SetTop(rectangle, part.Y * _squareSize);
            _gameBoard.Children.Add(rectangle);

        }

    }
}
