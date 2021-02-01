using System.Threading.Tasks;

public class CodeBlockKarelMove : CodeBlock
{
    public override string Name => "Move";

    protected override Task OnExecute(CodeBlockContext ctx)
        => ctx.KarelProgram.Robot.Move();
}