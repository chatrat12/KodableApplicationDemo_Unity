using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public abstract class CodeBlock
{
    public class CodeBlockEvent : UnityEvent<CodeBlock> { }
    public CodeBlockEvent ExecutionStarted { get; } = new CodeBlockEvent();
    public CodeBlockEvent ExecutionEnded { get; } = new CodeBlockEvent();

    public abstract string Name { get; }
    public virtual bool CanMove => true;
    public virtual Color Color => UIResources.CodeBlockColor;

    public async Task Execute(CodeBlockContext ctx)
    {
        ExecutionStarted.Invoke(this);
        await OnExecute(ctx);
        ExecutionEnded.Invoke(this);
    }
    protected abstract Task OnExecute(CodeBlockContext ctx);

    public virtual IEnumerable<CodeBlockBody> GetBodies() => Enumerable.Empty<CodeBlockBody>();
}