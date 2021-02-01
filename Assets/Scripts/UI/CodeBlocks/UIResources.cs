using UnityEngine;

[CreateAssetMenu(fileName = "UI Resources")]
public class UIResources : ScriptableObject
{
    public static UICodeBlock CodeBlockPrefab => GetInstance()._codeBlockPrefab;
    public static UICodeBlockBody BodyPrefab => GetInstance()._bodyPrefab;
    public static Color CodeBlockColor => GetInstance()._codeBlockColor;
    public static Color FunctionColor => GetInstance()._functionColor;
    public static Color CurrentStepColor => GetInstance()._currentStepColor;

    [SerializeField] private UICodeBlock _codeBlockPrefab;
    [SerializeField] private UICodeBlockBody _bodyPrefab;
    [SerializeField] private Color _codeBlockColor;
    [SerializeField] private Color _functionColor;
    [SerializeField] private Color _currentStepColor;

    private static UIResources _instance;

    private static UIResources GetInstance()
    {
        if (_instance == null)
            _instance = Resources.Load<UIResources>("UIResources");
        return _instance;
    }
}
