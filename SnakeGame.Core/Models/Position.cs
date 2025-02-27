namespace SnakeGame.Core.Models
{
    public class Position
    {
        public int X { get; }
        public int Y { get; }
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Position GetNextPosition(Direction direction) =>
            new Position(X + direction.DeltaX, Y + direction.DeltaY);

    }
}
