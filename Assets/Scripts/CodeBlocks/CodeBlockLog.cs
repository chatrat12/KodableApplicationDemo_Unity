
using System.Threading.Tasks;

public class CodeBlockLog : CodeBlock
{
    public override string Name => "Log";
    public CodeBlockParameter<string> Message { get; } = new CodeBlockParameter<string>();

    protected override Task OnExecute()
    {
        UnityEngine.Debug.Log(Message.GetValue());
        return Task.CompletedTask;
    }
}
