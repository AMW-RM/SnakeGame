using SnakeGame.Core.Interfaces;
using System.Windows;

namespace SnakeGame.Infrastructura;

public class WpfGameNotifications : IGameNotifications
{
    public void OnGameOver()
    {
        MessageBox.Show("Game Over. Naciśnij dowolny klawisz");
    }
}
