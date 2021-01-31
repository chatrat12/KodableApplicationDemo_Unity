using System.Threading.Tasks;

public class CodeBlockElseStatement : CodeBlock
{
    public override string Name => "if";
    public CodeBlockParameter<bool> Condition { get; } = new CodeBlockParameter<bool>();
    public CodeBlockBody ConditionPassedBody { get; } = new CodeBlockBody();
    public CodeBlockBody ConditionFailedBody { get; } = new CodeBlockBody();

    protected override async Task OnExecute()
    {
        if (await Condition.GetValue())
            await ConditionFailedBody.Execute();
        else
            await ConditionFailedBody.Execute();
    }
}
