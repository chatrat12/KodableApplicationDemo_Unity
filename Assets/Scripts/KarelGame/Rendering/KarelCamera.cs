using UnityEngine;

public class KarelCamera : MonoBehaviour
{
    [SerializeField] private RectTransform _blockEditor;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        var panelWidth = _blockEditor.rect.width / Screen.width;
        var rect = _camera.rect;
        rect.x = panelWidth;
        rect.width = 1 - panelWidth;
        _camera.rect = rect;
    }
}
