
using System.Collections.Generic;
using System.Threading.Tasks;

public class CodeBlockBody
{
    public IEnumerable<CodeBlock> Blocks => _childBlocks;

    private List<CodeBlock> _childBlocks = new List<CodeBlock>();

    public async Task Execute(CodeBlockContext ctx)
    {
        foreach (var block in _childBlocks)
            await block.Execute(ctx);
    }

    public void AddBlock(CodeBlock block)
    {
        _childBlocks.Add(block);
    }

    public void InsertBlock(int index, CodeBlock block)
    {
        _childBlocks.Insert(index, block);
    }

    public void RemoveBlock(CodeBlock block)
    {
        _childBlocks.Remove(block);
    }

    public void InsertAfter(CodeBlock block, CodeBlock blockToAdd)
        => InsertBlock(_childBlocks.IndexOf(block) + 1, blockToAdd);

    public void InsertBefore(CodeBlock block, CodeBlock blockToAdd)
        => InsertBlock(_childBlocks.IndexOf(block), blockToAdd);
}
