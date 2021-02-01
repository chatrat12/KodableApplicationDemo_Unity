using System.Threading.Tasks;
using UnityAsync;
using UnityEngine;

public class KarelComponent : MonoBehaviour
{
    public KarelInstance KarelInstance { get; private set; }

    [SerializeField] private WorldFile _worldFile;
    [SerializeField] private WorldRenderer _worldRenderer;
    [SerializeField] private KarelRenderer _karelRenderer;

    private void Awake()
    {
        KarelInstance = new KarelInstance(_worldFile);
        _worldRenderer.World = KarelInstance.World;
        _karelRenderer.Init(KarelInstance.Robot, _worldRenderer.GridOrigin);
    }

    public void Reset()
    {
        KarelInstance.Robot.Reset(_worldFile);
        KarelInstance.World.Reset(_worldFile);
    }

    private async void DoTest()
    {
        var robot = KarelInstance.Robot;
        await Await.Seconds(1);

        try
        {
            async Task TurnRight()
            {
                for (int i = 0; i < 3; i++)
                    await robot.TurnLeft();
            }
            async Task TurnAround()
            {
                for (int i = 0; i < 2; i++)
                    await robot.TurnLeft();
            }
            async Task Move(int count = 1)
            {
                for (int i = 0; i < count; i++)
                    await robot.Move();
            }

            await Move(2);
            await TurnRight();
            await Move();
            await robot.TurnLeft();
            await Move();
            await robot.PickBeeper();
            await TurnAround();
            await Move();
            await TurnRight();
            await Move();
            await robot.TurnLeft();
            await Move(2);
            await robot.PutBeeper();
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}
