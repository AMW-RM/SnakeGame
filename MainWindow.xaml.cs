using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace SnakeGame;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly int GameWidth = 20;
    private readonly int GameHeight = 20;
    private readonly int SquarSize = 20;

    //private readonly List<Point> SnakeParts = new List<Point>();
    private readonly List<Point> SnakeParts = new();//waz lista składowych
    private Point food; // jedzenie
    private Point currentDirection; //kierunek
    private bool gameOver; //koniec gry
    private DispatcherTimer gameTimer = new(); //czas gry, wątki graficzne obsługa działa na tym samym watku co user

    public MainWindow()
    {
        InitializeComponent();
        //inicjalizacja planszy
        GameBoard.Width = GameWidth * SquarSize;
        GameBoard.Height = GameHeight * SquarSize;
        //ustawienie timera i nterval 200ms, pętla 
        gameTimer.Interval = TimeSpan.FromMilliseconds(200);
        gameTimer.Tick += GameLoop;

        StartNewGame();
    }

    private void StartNewGame()
    {
        SnakeParts.Clear();
        gameOver = false;

        SnakeParts.Add(new Point(GameWidth / 2, GameHeight / 2));// srodek gry
        currentDirection = new Point(1, 0); //ustawienie kierunku w prawo.

        CreateFood();
        gameTimer.Start();

    }

    private void CreateFood()
    {
        Random rnd = new Random();

        do
        {
            food = new Point(rnd.Next(GameWidth), rnd.Next(GameHeight));
        } while (SnakeParts.Contains(food));
    }

    private void GameLoop(object? sender, EventArgs e)
    {
        if (gameOver) //(gameOver == true)
            return;
        //obliczanie pozycji głowy węza
        var newHead = new Point((SnakeParts[0].X + currentDirection.X + GameWidth) % GameWidth,
            (SnakeParts[0].Y + currentDirection.Y + GameHeight) % GameHeight);
        //sprawdzanie kolizji z samym sobą
        if (SnakeParts.Contains(newHead))
        {
            GameOver();
            return;
        };
        //dodanie nowej glowy
        SnakeParts.Insert(0, newHead);
        //sprawdzanie czy zjadł jedzenie
        if (newHead.Equals(food))
        {
            CreateFood();

        }
        else
        {
            SnakeParts.RemoveAt(SnakeParts.Count - 1);
        }
        Draw();
    }

    private void Draw()
    {
        GameBoard.Children.Clear();
        //rysujemy węża
        foreach (var part in SnakeParts)
        {
            var rect = new Rectangle
            {
                Width = SquarSize - 1,
                Height = SquarSize - 1,
                Fill = Brushes.Green
            };
            Canvas.SetLeft(rect, part.X * SquarSize);
            Canvas.SetTop(rect, part.Y * SquarSize);
            GameBoard.Children.Add(rect);
        }

        //rysujemy jedzenie
        var foodsape = new Ellipse
        {
            Width = SquarSize,
            Height = SquarSize,
            Fill = Brushes.Yellow
        };
        Canvas.SetLeft(foodsape, food.X * SquarSize);
        Canvas.SetTop(foodsape, food.Y * SquarSize);
        GameBoard.Children.Add(foodsape);
    }

    private void GameOver()
    {
        gameOver = true;
        gameTimer.Stop();
        MessageBox.Show("Game Over! - nacisnij Enter aby restartować");
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Left when currentDirection.X != 1:
                currentDirection = new Point(-1, 0);
                break;
            case Key.Right when currentDirection.X != -1:
                currentDirection = new Point(1, 0);
                break;
            case Key.Up when currentDirection.Y != 1:
                currentDirection = new Point(0, -1);
                break;
            case Key.Down when currentDirection.Y != -1:
                currentDirection = new Point(0, 1);
                break;
            case Key.Enter when gameOver:
                StartNewGame();
                break;
        }

    }

}