using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICodeBlock : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    public class CodeBlockDropEvent : UnityEvent<UICodeBlock> { }
    public CodeBlockDropEvent BlockDroppedBefore { get; } = new CodeBlockDropEvent();
    public CodeBlockDropEvent BlockDroppedAfter { get; } = new CodeBlockDropEvent();
    public UnityEvent ClickedLeft { get; } = new UnityEvent();


    [SerializeField] UIDropArea _beforeDropArea;
    [SerializeField] UIDropArea _afterDropArea;

    public CodeBlock CodeBlock
    {
        get => _codeBlock;
        set
        {
            _codeBlock = value;
            _name.text = value.Name;
            AddBodies();
            UpdateRenderers();
            Color = value.Color;
            SetDropAreasActive(value.CanMove);
            value.ExecutionStarted.AddListener((sender) =>
            {
                if (value.CanMove)
                    Color = UIResources.CurrentStepColor;
            });
            value.ExecutionEnded.AddListener((sender) =>
            {
                if (value.CanMove)
                    Color = sender.Color;
            });
        }
    }

    public Color Color
    {
        get
        {
            if (_renderers != null && _renderers.Length > 0)
                return _renderers[0].color;
            return Color.white;
        }
        set
        {
            if(_renderers != null)
            {
                foreach (var renderer in _renderers)
                    renderer.color = value;
            }
        }
    }


    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Transform _bodiesParent;

    private Image[] _renderers;
    private CodeBlock _codeBlock;

    private void Awake()
    {
        _beforeDropArea.BlockDropped.AddListener(() => BlockDroppedBefore.Invoke(GameCursor.CodeBlocks[0]));
        _afterDropArea.BlockDropped.AddListener(() => BlockDroppedAfter.Invoke(GameCursor.CodeBlocks[0]));
    }

    public void OnPointerClick(PointerEventData eventData) { }// => ClickedLeft.Invoke();

    public void SetDropAreasActive(bool active)
    {

        _afterDropArea.gameObject.SetActive(active);
        _beforeDropArea.gameObject.SetActive(active);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ClickedLeft.Invoke();
    }

    private void AddBodies()
    {
        foreach(var body in _codeBlock.GetBodies())
        {
            var bodyUI = Instantiate(UIResources.BodyPrefab, _bodiesParent);
            bodyUI.Body = body;
        }
    }

    private void UpdateRenderers()
    {
        _renderers = GetComponentsInChildren<Image>();
    }
}