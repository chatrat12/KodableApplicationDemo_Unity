using System.Threading.Tasks;

public class CodeBlockElseStatement : CodeBlock
{
    public override string Name => "if";
    public CodeBlockParameter<bool> Condition { get; } = new CodeBlockParameter<bool>();
    public CodeBlockBody ConditionPassedBody { get; } = new CodeBlockBody();
    public CodeBlockBody ConditionFailedBody { get; } = new CodeBlockBody();

    protected override async Task OnExecute(CodeBlockContext ctx)
    {
        if (await Condition.GetValue(ctx))
            await ConditionFailedBody.Execute(ctx);
        else
            await ConditionFailedBody.Execute(ctx);
    }
}
