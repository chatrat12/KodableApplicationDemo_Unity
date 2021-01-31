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

    public KarelRobot(KarelInstance program, WorldFile file) : this(program, file.KarelStartPosition, file.KarelStartDirection)
    {
        BeeperBag = file.StartingBeepers;
    }

    public async Task Move()
    {
        if (_program.World.IsBlocked(this))
            throw new System.Exception("Karel crashed.");
        else
        {
            Position += Direction.ToVector();
            PositionChanged?.Invoke(this);
            await Await.Seconds(_program.StepTime);
        }
    }

    public async Task TurnLeft()
    {
        Direction = Direction.RotateCounterClockwise();
        DirectionChanged?.Invoke(this);
        await Await.Seconds(_program.StepTime);
    }

    public async Task PutBeeper()
    {
        if (BeeperBag > 0)
        {
            BeeperBag--;
            _program.World.PlaceBeeper(Position);
            await Await.Seconds(_program.StepTime);
        }
        else
            throw new System.Exception("No beeper in beeper bag to place. :(");
    }
    public async Task PickBeeper()
    {
        if (_program.World.GetBeeperCount(Position) > 0)
        {
            BeeperBag++;
            _program.World.RemoveBeeper(Position);
            await Await.Seconds(_program.StepTime);
        }
        else
            throw new System.Exception("No beeper to pick up. :(");
    }
}
