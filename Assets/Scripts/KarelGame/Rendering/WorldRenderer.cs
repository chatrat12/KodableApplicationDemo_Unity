using System.Collections.Generic;
using UnityEngine;

public class WorldRenderer : MonoBehaviour
{
    public KarelWorld World
    {
        get => _world;
        set
        {
            _world = value;
            Init(value);
        }
    }
    public Matrix4x4 GridOrigin => _gridOrigin;

    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private SpriteRenderer _gridPointPrefab;
    [SerializeField] private SpriteRenderer _wallPrefab;
    [SerializeField] private SpriteRenderer _beeperPrefab;

    private KarelWorld _world;
    private Matrix4x4 _gridOrigin;

    private Dictionary<Vector2Int, SpriteRenderer> _beeperRenderers = new Dictionary<Vector2Int, SpriteRenderer>();

    private void Init(KarelWorld world)
    {
        _background.transform.localScale = new Vector3(world.Size.x, world.Size.y, 1);
        var origin = Matrix4x4.Translate(-(Vector2)world.Size * 0.5f);
        _gridOrigin = origin * Matrix4x4.Translate(Vector2.one * 0.5f);

        AddGrid(world, _gridOrigin);
        AddWalls(world.GetHorizontalWalls(), origin, true);
        AddWalls(world.GetVerticalWalls(), origin, false);
        AddBeepers(world.GetBeeperPositions(), _gridOrigin);

        world.BeeperCountChanged += (pos, count) 
            => SetBeeper(pos, count, _gridOrigin);
    }

    private void AddGrid(KarelWorld world, Matrix4x4 origin)
    {
        for (int y = 0; y < world.Size.y; y++)
        {
            for (int x = 0; x < world.Size.x; x++)
            {
                var point = Instantiate(_gridPointPrefab, this.transform);
                point.transform.position = origin.MultiplyPoint(new Vector2(x, y));
            }
        }
    }

    private void AddWalls(IEnumerable<Vector2Int> walls, Matrix4x4 origin, bool horizontal)
    {
        if (horizontal)
            origin *= Matrix4x4.Translate(new Vector2(0.5f, 1f));
        else
            origin *= Matrix4x4.Translate(new Vector2(1f, 0.5f));

        foreach (var pos in walls)
        {
            var wall = Instantiate(_wallPrefab, this.transform);
            if (horizontal)
                wall.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            wall.transform.position = origin.MultiplyPoint((Vector2)pos);
        }
    }

    private void AddBeepers(IEnumerable<Vector2Int> positions, Matrix4x4 origin)
    {
        foreach(var pos in positions)
            SetBeeper(pos, 1, origin);
    }

    private void SetBeeper(Vector2Int pos, int count, Matrix4x4 matrix)
    {
        if(count > 0)
        {
            if(!_beeperRenderers.ContainsKey(pos))
            {
                var beeper = Instantiate(_beeperPrefab, this.transform);
                beeper.transform.position = matrix.MultiplyPoint((Vector2)pos);
                _beeperRenderers.Add(pos, beeper);
            }
        }
        else
        {
            if(_beeperRenderers.ContainsKey(pos))
            {
                Destroy(_beeperRenderers[pos].gameObject);
                _beeperRenderers.Remove(pos);
            }
        }
    }
}
