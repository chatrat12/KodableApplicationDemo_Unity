using UnityEngine;
using UnityEngine.UI;

public class UIDropIndicator : MonoBehaviour
{
    private RectTransform _rect;
    private UIDropArea _currentDropArea;
    private Image _image;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _image.raycastTarget = false;
        UpdateIndicator();
    }

    private void Update()
    {
        _image.enabled = _currentDropArea != null && GameCursor.CodeBlocks != null && GameCursor.CodeBlocks.Length > 0;
        if (GameCursor.DropArea != _currentDropArea)
        {
            _currentDropArea = GameCursor.DropArea;
            UpdateIndicator();
        }
    }

    private void UpdateIndicator()
    {
        if (_currentDropArea != null)
        {
            _rect.transform.position = _currentDropArea.transform.position;
            var size = _rect.sizeDelta;
            size.x = _currentDropArea.Width;
            _rect.sizeDelta = size;
        }
    }
}
