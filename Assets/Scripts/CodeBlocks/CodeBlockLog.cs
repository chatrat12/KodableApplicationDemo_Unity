
using System.Threading.Tasks;

public class CodeBlockLog : CodeBlock
{
    public override string Name => "Log";
    public CodeBlockParameter<string> Message { get; } = new CodeBlockParameter<string>();

    protected override Task OnExecute(CodeBlockContext ctx)
    {
        UnityEngine.Debug.Log(Message.GetValue(ctx));
        return Task.CompletedTask;
    }
}
