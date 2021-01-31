using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICodeBlock : MonoBehaviour, IPointerClickHandler
{
    public CodeBlock CodeBlock
    {
        get => _codeBlock;
        set
        {
            _codeBlock = value;
            _name.text = value.Name;
        }
    }

    public Color Color
    {
        get => _panel.color;
        set
        {
            _panel.color = value;
            _outputTab.color = value;
        }
    }

    public UnityEvent ClickedLeft { get; } = new UnityEvent();

    [SerializeField] private Image _panel;
    [SerializeField] private Image _outputTab;
    [SerializeField] private TextMeshProUGUI _name;

    private CodeBlock _codeBlock;

    public void OnPointerClick(PointerEventData eventData) => ClickedLeft.Invoke();
}
