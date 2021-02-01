using System.Threading.Tasks;

public class CodeBlockKarelTurnLeft : CodeBlock
{
    public override string Name => "TurnLeft";

    protected override Task OnExecute(CodeBlockContext ctx)
        => ctx.KarelProgram.Robot.TurnLeft();
}
