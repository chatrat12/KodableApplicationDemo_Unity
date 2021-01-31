using System.Threading.Tasks;
using UnityAsync;
using UnityEngine;

public class KarolTest : MonoBehaviour
{
    [SerializeField] private WorldFile _worldFile;
    [SerializeField] private WorldRenderer _worldRenderer;
    [SerializeField] private KarelRenderer _karelRenderer;

    private async void Awake()
    {
        var instance = new KarelInstance(_worldFile);
        _worldRenderer.World = instance.World;
        _karelRenderer.Init(instance.Robot, _worldRenderer.GridOrigin);

        var robot = instance.Robot;
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
