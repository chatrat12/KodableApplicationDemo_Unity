public class KarelInstance
{
    public KarelWorld World { get; private set; }
    public KarelRobot Robot { get; private set; }
    public float StepTime { get; set; } = 0.5f;

    public KarelInstance(WorldFile file)
    {
        Robot = new KarelRobot(this, file);
        World = new KarelWorld(file);
    }

    public void Crash(string msg)
    {
        UnityEngine.Debug.Log($"Crash: {msg}");
    }
}
