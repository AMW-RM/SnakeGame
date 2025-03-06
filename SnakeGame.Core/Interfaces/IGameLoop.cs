namespace SnakeGame.Core.Interfaces;

public interface IGameLoop
{
    void Start();
    void Stop();
    void SetSpeed(TimeSpan interval);

    bool IsRunning { get; }
}
