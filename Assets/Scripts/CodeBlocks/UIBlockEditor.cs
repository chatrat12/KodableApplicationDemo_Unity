using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBlockEditor : MonoBehaviour
{
    public UnityEvent ProgramComplete { get; } = new UnityEvent();

    [SerializeField] private Transform _blockArea;
    [SerializeField] private KarelComponent _karelComponent;

    private CodeBlockMain _mainBlock;
    private CodeBlockContext _ctx;

    private void Start()
    {
        _ctx = new CodeBlockContext() { KarelProgram = _karelComponent.KarelInstance } ;
        _mainBlock = new CodeBlockMain();
        var ui = Instantiate(UIResources.CodeBlockPrefab, _blockArea);
        ui.CodeBlock = _mainBlock;
    }

    public async void StartProgram()
    {
        _karelComponent.Reset();
        try
        {
            await _mainBlock.Execute(_ctx);
            ProgramComplete.Invoke();
        }
        catch(Exception e)
        {
            Debug.Log($"Crash: {e.Message}");
            ProgramComplete.Invoke();
        }
    }
}
