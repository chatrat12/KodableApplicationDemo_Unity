using UnityEngine;

public class GameCursor
{
    public delegate void CodeBlockChangeEvent(UICodeBlock[] blocks);
    public static event CodeBlockChangeEvent BlocksChanged;

    public static UICodeBlock[] CodeBlocks
    {
        get => _codeBlocks;
        set
        {
            _codeBlocks = value;
            BlocksChanged?.Invoke(value);
        }
    }


    private static UICodeBlock[] _codeBlocks;

    public static void ClearBlocks()
    {
        foreach (var block in _codeBlocks)
            GameObject.Destroy(block.gameObject);
        _codeBlocks = new UICodeBlock[0];
    }
}
