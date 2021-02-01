using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIDropArea : NonDrawingGraphic, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 AnchoredPosition => _rect.anchoredPosition;
    public float Width => _rect.rect.width;

    private RectTransform _rect;

    protected override void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    public UnityEvent BlockDropped { get; } = new UnityEvent();

    public void OnPointerEnter(PointerEventData eventData)
        => GameCursor.DropArea = this;

    public void OnPointerExit(PointerEventData eventData)
        => GameCursor.DropArea = null;

    public void OnBlockDropped()
        => BlockDropped.Invoke();
}
