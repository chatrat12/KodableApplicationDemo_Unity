using System;
using System.Collections.Generic;
using UnityEngine;

public class KarelWorld
{
    public delegate void BeeperEvent(Vector2Int position, int beeperCount);
    public event BeeperEvent BeeperCountChanged;

    public Vector2Int Size { get; private set; }
    private int[,] _beepers;
    private bool[,] _horizontalWalls;
    private bool[,] _verticalWalls;

    public KarelWorld(int columns, int rows)
    {
        Size = new Vector2Int(columns, rows);
        _beepers = new int[columns, rows];
        _horizontalWalls = new bool[columns, rows - 1];
        _verticalWalls = new bool[columns - 1, rows];
    }

    public KarelWorld(WorldFile file) : this(file.Size.x, file.Size.y)
    {
        foreach (var wall in file.HorzontalWalls)
            _horizontalWalls[wall.x, wall.y] = true;
        foreach (var wall in file.VerticalWalls)
            _verticalWalls[wall.x, wall.y] = true;
        foreach (var beeper in file.Beepers)
            _beepers[beeper.Position.x, beeper.Position.y] = beeper.Count;
    }

    internal void Reset(WorldFile worldFile)
    {
        for (int y = 0; y < _beepers.GetLength(1); y++)
        {
            for (int x = 0; x < _beepers.GetLength(0); x++)
            {
                _beepers[x, y] = 0;
                BeeperCountChanged.Invoke(new Vector2Int(x, y), 0);
            }
        }
        foreach (var beeper in worldFile.Beepers)
        {
            _beepers[beeper.Position.x, beeper.Position.y] = beeper.Count;
                BeeperCountChanged.Invoke(beeper.Position, beeper.Count);
        }
    }

    public void PlaceBeeper(Vector2Int position)
    {
        _beepers[position.x, position.y]++;
        BeeperCountChanged?.Invoke(position, _beepers[position.x, position.y]);
    }

    public void RemoveBeeper(Vector2Int position)
    {
        _beepers[position.x, position.y]--;
        BeeperCountChanged?.Invoke(position, _beepers[position.x, position.y]);
    }
    public int GetBeeperCount(Vector2Int position)
        => _beepers[position.x, position.y];

    public bool IsBlocked(Vector2Int position, Direction direction)
    {
        var destination = position + direction.ToVector();
        if (!IsPositionInBounds(destination))
            return true;
        switch (direction)
        {
            case Direction.North: return _horizontalWalls[position.x, position.y];
            case Direction.South: return _horizontalWalls[position.x, position.y - 1];
            case Direction.West: return _verticalWalls[position.x - 1, position.y];
            case Direction.East: return _verticalWalls[position.x, position.y];
            default: return true;
        }
    }
    public bool IsBlocked(KarelRobot robot) => IsBlocked(robot.Position, robot.Direction);

    public bool IsPositionInBounds(Vector2Int position)
    {
        return position.x >= 0 && position.x < Size.x &&
               position.y >= 0 && position.y < Size.y;
    }

    public IEnumerable<Vector2Int> GetHorizontalWalls() => GetWalls(_horizontalWalls);
    public IEnumerable<Vector2Int> GetVerticalWalls() => GetWalls(_verticalWalls);

    private IEnumerable<Vector2Int> GetWalls(bool[,] walls)
    {
        for (int y = 0; y < walls.GetLength(1); y++)
        {
            for (int x = 0; x < walls.GetLength(0); x++)
            {
                if (walls[x, y])
                    yield return new Vector2Int(x, y);
            }
        }
    }

    public IEnumerable<Vector2Int> GetBeeperPositions()
    {
        for (int y = 0; y < _beepers.GetLength(1); y++)
        {
            for (int x = 0; x < _beepers.GetLength(0); x++)
            {
                if (_beepers[x, y] > 0)
                    yield return new Vector2Int(x, y);
            }
        }
    }
}
