using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CodeBlockMain : CodeBlock
{
    public override string Name => "Main";
    public CodeBlockBody Body { get; } = new CodeBlockBody();
    public override bool CanMove => false;
    public override Color Color => UIResources.FunctionColor;

    protected override Task OnExecute(CodeBlockContext ctx) => Body.Execute(ctx);
    public override IEnumerable<CodeBlockBody> GetBodies() { yield return Body; }
}
