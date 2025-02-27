namespace SnakeGame.Core.Models
{
    internal class Snake
    {
        private readonly List<Position> _snakeParts = new();
        public IReadOnlyList<Position> Parts => _snakeParts;

        public Position Head => _snakeParts[0];

        public Direction CurrentDirection { get; set; }

        public Snake(Position startPosition, Direction startDirection)
        {
            _snakeParts.Add(startPosition);
            CurrentDirection = startDirection;
        }

        public void Move(Position newHead)
        {
            _snakeParts.Insert(0, newHead);
        }

        public void RemoveTail()
        { 
            _snakeParts.RemoveAt(_snakeParts.Count - 1);
        }

        public bool CollidesWith(Position newHead) => _snakeParts.Contains(newHead);

    }
}
