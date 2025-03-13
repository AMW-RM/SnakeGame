using SnakeGame.Core.Game;
using SnakeGame.Core.Interfaces;
using SnakeGame.Core.Models;
using SnakeGame.Infrastructura;
using SnakeGame.Reanders;
using System.Windows;
using System.Windows.Input;


namespace SnakeGame;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly int GameWidth = 20;
    private readonly int GameHeight = 20;
    private readonly int SquarSize = 20;
    private readonly GameEngine _engine;
    private readonly IGameState _state;
    private readonly IGameLoop _gameLoop;

    public MainWindow()
    {
        InitializeComponent();
        _state = new GameState(GameWidth, GameHeight);
        var render = new WpfGameReander(GameBoard, SquarSize, GameWidth, GameHeight);
        var foodGeneator = new RandomFoodGenerator();
        var notification = new WpfGameNotifications();
        _engine = new GameEngine(_state, render, foodGeneator, notification);
        _gameLoop = new WpfGameLoop(_engine);
        _gameLoop.Start();

    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        Direction newDirection = e.Key switch
        {
            Key.Left => Direction.Left,
            Key.Right => Direction.Right,
            Key.Up => Direction.Up,
            Key.Down => Direction.Down,
            _ => _state.Snake.CurrentDirection
        };

        if (e.Key == Key.Enter && _state.IsGameOver)
        {
            RestartGame();
            return;
        }

        _engine.ChangeDirection(newDirection);
    }

    private void RestartGame()
    {
        _engine.Reset();
        _gameLoop.Start();
    }

}

