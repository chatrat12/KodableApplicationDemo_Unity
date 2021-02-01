using System.Linq;
using UnityEngine;

public class UICodeBlockBody : MonoBehaviour
{
    public CodeBlockBody Body
    {
        get => _body;
        set => _body = value;
    }

    [SerializeField] UIDropArea _emtpyDropArea;
    [SerializeField] Transform _blocksParent;
    private CodeBlockBody _body;

    private void Awake()
    {
        _emtpyDropArea.BlockDropped.AddListener(() => 
        {
            InsertBlock(0, GameCursor.CodeBlocks[0]);
        });
    }

    public void InsertBlock(int index, UICodeBlock block)
    {
        block.transform.SetParent(_blocksParent);
        block.transform.SetSiblingIndex(index);
        block.BlockDroppedAfter.AddListener((blockToAdd) => InsertBlockAfter(block, blockToAdd));
        block.BlockDroppedBefore.AddListener((blockToAdd) => InsertBlockBefore(block, blockToAdd));
        _body.InsertBlock(index, block.CodeBlock);
        block.ClickedLeft.RemoveAllListeners();
        block.ClickedLeft.AddListener(() =>
        {
            if (GameCursor.CodeBlocks == null || GameCursor.CodeBlocks.Length == 0)
                GameCursor.CodeBlocks = new UICodeBlock[] { RemoveBlock(block) };
        });

        UpdateEmptyDropArea();
    }

    public void InsertBlockAfter(UICodeBlock block, UICodeBlock blockToAdd)
    {
        var index = block.transform.GetSiblingIndex();
        InsertBlock(index + 1, blockToAdd);
    }

    public void InsertBlockBefore(UICodeBlock block, UICodeBlock blockToAdd)
    {
        var index = block.transform.GetSiblingIndex();
        InsertBlock(index, blockToAdd);
    }

    public UICodeBlock RemoveBlock(UICodeBlock block)
    {
        var result = _blocksParent.OfType<Transform>().Select(t => t.GetComponent<UICodeBlock>()).First(b => b == block);
        _body.RemoveBlock(result.CodeBlock);
        result.BlockDroppedAfter.RemoveAllListeners();
        result.BlockDroppedBefore.RemoveAllListeners();
        UpdateEmptyDropArea();
        return result;
    }

    private void UpdateEmptyDropArea()
    {
        _emtpyDropArea.gameObject.SetActive(!_body.Blocks.Any());
        if (!_emtpyDropArea.gameObject.activeSelf && GameCursor.DropArea == _emtpyDropArea)
            GameCursor.DropArea = null;
    }
}
