using System.Threading.Tasks;

public class MainBlock : CodeBlock
{
    public override string Name => "Main";
    public CodeBlockBody Body { get; } = new CodeBlockBody();

    protected override Task OnExecute() => Body.Execute();
}
