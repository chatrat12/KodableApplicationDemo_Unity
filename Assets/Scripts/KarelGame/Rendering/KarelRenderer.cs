using UnityEngine;

public class KarelRenderer : MonoBehaviour
{
    private KarelRobot _robot;
    private Matrix4x4 _gridMatrix;

    public void Init(KarelRobot robot, Matrix4x4 gridMatrix)
    {
        _robot = robot;
        _gridMatrix = gridMatrix;

        UpdatePosition();
        UpdateRotation();

        robot.PositionChanged += (sender) => UpdatePosition();
        robot.DirectionChanged += (sender) => UpdateRotation();
    }

    private void UpdatePosition() 
        => transform.position = _gridMatrix.MultiplyPoint((Vector2)_robot.Position);
    private void UpdateRotation()
        => transform.rotation = GetRotation(_robot.Direction);

    private Quaternion GetRotation(Direction direction)
    {
        switch(direction)
        {
            case Direction.North: return Quaternion.AngleAxis(90, Vector3.forward);
            case Direction.South: return Quaternion.AngleAxis(-90, Vector3.forward);
            case Direction.West: return Quaternion.AngleAxis(180, Vector3.forward);
            default: return Quaternion.identity;
        }
    }
    
}