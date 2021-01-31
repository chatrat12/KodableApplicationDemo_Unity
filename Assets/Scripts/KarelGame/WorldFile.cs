using UnityEngine;

public class WorldFile : ScriptableObject
{
    public Vector2Int KarelStartPosition => _karelStartPosition;
    public Direction KarelStartDirection => _karelStartDirection;
    public int StartingBeepers => _startingBeepers;
    public Vector2Int Size => _size;
    public Vector2Int[] HorzontalWalls => _horzontalWalls;
    public Vector2Int[] VerticalWalls => _verticalWalls;
    public Beeper[] Beepers => _beepers;

    [SerializeField] public Vector2Int _karelStartPosition = Vector2Int.zero;
    [SerializeField] public Direction _karelStartDirection = Direction.East;
    [SerializeField] public int _startingBeepers = 0;
    [SerializeField] public Vector2Int _size;
    [SerializeField] public Vector2Int[] _horzontalWalls;
    [SerializeField] public Vector2Int[] _verticalWalls;
    [SerializeField] public Beeper[] _beepers;

    [System.Serializable]
    public class Beeper
    {
        public Vector2Int Position => _position;
        public int Count => _count;

        [SerializeField] public Vector2Int _position;
        [SerializeField] public int _count;
    }

}
