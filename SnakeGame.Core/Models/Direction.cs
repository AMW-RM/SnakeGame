namespace SnakeGame.Core.Models
{
    public class Direction
    {
        public static readonly Direction Left = new(-1, 0);
        public static readonly Direction Right = new(1, 0);
        public static readonly Direction Up = new(0, -1);
        public static readonly Direction Down = new(0, 1);

        public int DeltaX { get; private set; }
        public int DeltaY { get; private set; }

        public Direction(int deltaX, int deltaY)
        {
            DeltaX = deltaX;
            DeltaY = deltaY;
        }
        public Direction GetOpposite() => 
            new Direction(-DeltaX, -DeltaY);

        /*
        public Direction GetOpposite()
        {
            return new Direction(-DeltaX, -DeltaY);
        }
        */
    }
}