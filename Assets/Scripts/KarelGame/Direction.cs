using UnityEngine;

public enum Direction
{
    North,
    East,
    South,
    West
}

public static class DirectionExtensions
{
    public static Direction RotateClockwise(this Direction direction)
    {
        direction++;
        if (direction > Direction.West)
            direction = Direction.North;
        return direction;
    }

    public static Direction RotateCounterClockwise(this Direction direction)
    {
        direction--;
        if (direction < Direction.North)
            direction = Direction.West;
        return direction;
    }

    public static Vector2Int ToVector(this Direction direction)
    {
        switch(direction)
        {
            case Direction.North: return Vector2Int.up;
            case Direction.East: return Vector2Int.right;
            case Direction.South: return Vector2Int.down;
            case Direction.West: return Vector2Int.left;
            default: return Vector2Int.zero;
        }
    }
}