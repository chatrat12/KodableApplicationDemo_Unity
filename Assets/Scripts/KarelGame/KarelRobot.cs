using System;
using System.Threading.Tasks;
using UnityAsync;
using UnityEngine;

public class KarelRobot
{
    public delegate void KarelEvent(KarelRobot sender);
    public event KarelEvent PositionChanged;
    public event KarelEvent DirectionChanged;

    public Direction Direction { get; private set; } = Direction.East;
    public Vector2Int Position { get; private set; }
    public int BeeperBag { get; private set; }
    private KarelInstance _program;

    public KarelRobot(KarelInstance program, Vector2Int position, Direction direction)
    {
        _program = program;
        Position = position;
        Direction = direction;
    }

    internal void Reset(WorldFile worldFile)
    {
        Position = worldFile.KarelStartPosition;
        Direction = worldFile.KarelStartDirection;
        PositionChanged.Invoke(this);
        DirectionChanged.Invoke(this);
    }

    public KarelRobot(KarelInstance program, WorldFile file) : this(program, file.KarelStartPosition, file.KarelStartDirection)
    {
        BeeperBag = file.StartingBeepers;
    }

    public async Task Move()
    {
        if (_program.World.IsBlocked(this))
            throw new Exception("Karel crashed.");
        else
        {
            await _program.AwaitNextStep();
            Position += Direction.ToVector();
            PositionChanged?.Invoke(this);
        }
    }

    public async Task TurnLeft()
    {
        await _program.AwaitNextStep();
        Direction = Direction.RotateCounterClockwise();
        DirectionChanged?.Invoke(this);
    }

    public async Task PutBeeper()
    {
        if (BeeperBag > 0)
        {
            await _program.AwaitNextStep();
            BeeperBag--;
            _program.World.PlaceBeeper(Position);
        }
        else
            throw new Exception("No beeper in beeper bag to place. :(");
    }
    public async Task PickBeeper()
    {
        if (_program.World.GetBeeperCount(Position) > 0)
        {
            await _program.AwaitNextStep();
            BeeperBag++;
            _program.World.RemoveBeeper(Position);
        }
        else
            throw new Exception("No beeper to pick up. :(");
    }
}
