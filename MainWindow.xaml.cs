using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;


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
    private TextBlock scoreText;
    private int score;

    public MainWindow()
    {
        InitializeComponent();
        //inicjalizacja planszy
        GameBoard.Width = GameWidth * SquarSize;
        GameBoard.Height = GameHeight * SquarSize;
        //ustawienie timera i nterval 200ms, pętla 
        gameTimer.Interval = TimeSpan.FromMilliseconds(200);
        gameTimer.Tick += GameLoop;
        scoreText = new TextBlock
        {
            FontSize = 16,
            Foreground = Brushes.White,
            Margin = new Thickness(10)
        };
        GameBoard.Children.Add(scoreText);

        StartNewGame();
    }

    private void StartNewGame()
    {
        SnakeParts.Clear();
        gameOver = false;
        score = 0;
        SnakeParts.Add(new Point(GameWidth / 2, GameHeight / 2));// srodek gry
        currentDirection = new Point(1, 0); //ustawienie kierunku w prawo.

        CreateFood();
        UpdateScore();
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
        //obliczanie pozycji głowy węza - wersja przenikania
        /*
        var newHead = new Point((SnakeParts[0].X + currentDirection.X + GameWidth) % GameWidth,
            (SnakeParts[0].Y + currentDirection.Y + GameHeight) % GameHeight);
        */
        //obliczanie pozycji głowy węza - kolizja z krawędzią, koniec gry
        /*
        var newHead = new Point(SnakeParts[0].X + currentDirection.X,
            SnakeParts[0].Y + currentDirection.Y);
        // Sprawdzenie kolizji z krawędziami
        if (newHead.X < 0 || newHead.X >= GameWidth || newHead.Y < 0 || newHead.Y >= GameHeight)
        {
            GameOver();
            return;
        }
        */
        //obliczanie pozycji głowy węza - wersja odbicie od sciany
        var newHead = new Point(SnakeParts[0].X + currentDirection.X, SnakeParts[0].Y + currentDirection.Y);
        // Sprawdzenie kolizji z krawędziami i odbicie
        if (newHead.X < 0 || newHead.X >= GameWidth)
        {
            currentDirection = new Point(-currentDirection.X, currentDirection.Y); // Odbicie w poziomie
            newHead = new Point(SnakeParts[0].X + currentDirection.X, SnakeParts[0].Y); // Przesunięcie w przeciwnym kierunku
        }

        if (newHead.Y < 0 || newHead.Y >= GameHeight)
        {
            currentDirection = new Point(currentDirection.X, -currentDirection.Y); // Odbicie w pionie
            newHead = new Point(SnakeParts[0].X, SnakeParts[0].Y + currentDirection.Y); // Przesunięcie w przeciwnym kierunku
        }


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
            score++;
            CreateFood();

        }
        else
        {
            SnakeParts.RemoveAt(SnakeParts.Count - 1);
        }
        UpdateScore();  
        Draw();
    }
    private void UpdateScore()
    {
        scoreText.Text = $"Score: {score}";
    }
    private void Draw()
    {
        GameBoard.Children.Clear();
        GameBoard.Children.Add(scoreText);
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
        MessageBox.Show($"Twoje punkty: {score}");
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