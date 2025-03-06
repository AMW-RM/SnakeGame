using SnakeGame.Core.Game;
using SnakeGame.Core.Interfaces;
using System.Windows.Threading;

namespace SnakeGame.Infrastructura;


internal class WpfGameLoop : IGameLoop
{
    private readonly GameEngine _gameEngine;
    private readonly DispatcherTimer _timer;
    private readonly int _startInterval = 200;


    public WpfGameLoop(GameEngine gameEngine)
    {
        _gameEngine = gameEngine;
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(_startInterval)
        };
        _timer.Tick += (s, e) => gameEngine.Update();
    }
    public bool IsRunning => _timer.IsEnabled;

    public void SetSpeed(TimeSpan interval)
    {
        _timer.Interval = interval;
    }

    public void Start()
    {
        _timer.Start();
    }
    public void Stop() => _timer.Stop();
}
