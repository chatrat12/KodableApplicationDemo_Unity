using UnityEngine;

public class UICodeBlockPicker : MonoBehaviour
{
    private void Start()
    {
        CreateCodeBlockUI(new CodeBlockKarelMove());
        CreateCodeBlockUI(new CodeBlockKarelTurnLeft());
        CreateCodeBlockUI(new CodeBlockKarelPickBeeper());
        CreateCodeBlockUI(new CodeBlockKarelPutBeeper());
    }

    private UICodeBlock CreateCodeBlockUI(CodeBlock block)
    {
        var ui = Instantiate(UIResources.CodeBlockPrefab, this.transform);
        ui.CodeBlock = block;
        ui.SetDropAreasActive(false);
        ui.ClickedLeft.AddListener(() =>
        {
            var instance = Instantiate(ui);
            instance.CodeBlock = GetBlockInstance(block);
            instance.SetDropAreasActive(instance.CodeBlock.CanMove);
            if (GameCursor.CodeBlocks != null && GameCursor.CodeBlocks.Length > 0)
                GameCursor.ClearBlocks();
            else
                GameCursor.CodeBlocks = new UICodeBlock[] { instance };
        });
        return ui;
    }

    // TODO: Refactor ugly hack
    private CodeBlock GetBlockInstance(CodeBlock block)
    {
        if (block is CodeBlockKarelMove)
            return new CodeBlockKarelMove();
        if (block is CodeBlockKarelTurnLeft)
            return new CodeBlockKarelTurnLeft();
        if (block is CodeBlockKarelPickBeeper)
            return new CodeBlockKarelPickBeeper();
        if (block is CodeBlockKarelPutBeeper)
            return new CodeBlockKarelPutBeeper();
        return null;
    }
}
