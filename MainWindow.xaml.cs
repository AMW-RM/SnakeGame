using System.Windows;
using System.Windows.Media;
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
       
    }

    private void GameOver()
    {
        throw new NotImplementedException();
    }
}