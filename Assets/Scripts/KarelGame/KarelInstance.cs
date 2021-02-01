using System.Threading.Tasks;
using UnityAsync;
using UnityEngine;

public class KarelInstance
{
    public KarelWorld World { get; private set; }
    public KarelRobot Robot { get; private set; }
    public float StepTime { get; set; } = 0.5f;
    public bool Paused { get; set; } = false;

    public KarelInstance(WorldFile file)
    {
        Robot = new KarelRobot(this, file);
        World = new KarelWorld(file);
    }

    public void Crash(string msg)
    {
        UnityEngine.Debug.Log($"Crash: {msg}");
    }

    public async Task AwaitNextStep()
    {
        float percent = 0;
        await Await.Until(() =>
        {
            if(!Paused)
                percent += Time.deltaTime / StepTime;
            return percent >= 1;
        });
    }
}
