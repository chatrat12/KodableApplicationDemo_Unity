using UnityEngine;

public class UICodeBlockPicker : MonoBehaviour
{
    private void Start()
    {
        CreateCodeBlockUI(new CodeBlockLog());
    }

    private UICodeBlock CreateCodeBlockUI(CodeBlock block)
    {
        var ui = Instantiate(UIResources.CodeBlockPrefab, this.transform);
        ui.CodeBlock = block;
        ui.ClickedLeft.AddListener(() =>
        {
            var instance = Instantiate(ui);
            if (GameCursor.CodeBlocks != null && GameCursor.CodeBlocks.Length > 0)
                GameCursor.ClearBlocks();
            else
                GameCursor.CodeBlocks = new UICodeBlock[] { instance };
            Debug.Log("Clicked");
        });
        return ui;
    }
}
