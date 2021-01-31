using UnityEngine;

[CreateAssetMenu(fileName = "UI Resources")]
public class UIResources : ScriptableObject
{
    public static UICodeBlock CodeBlockPrefab => GetInstance()._codeBlockPrefab;
    public static Color CodeBlockColor => GetInstance()._codeBlockColor;

    [SerializeField] private UICodeBlock _codeBlockPrefab;
    [SerializeField] private Color _codeBlockColor;

    private static UIResources _instance;

    private static UIResources GetInstance()
    {
        if (_instance == null)
            _instance = Resources.Load<UIResources>("UIResources");
        return _instance;
    }
}
