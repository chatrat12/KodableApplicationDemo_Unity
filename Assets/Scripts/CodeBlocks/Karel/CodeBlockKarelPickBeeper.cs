using System.Threading.Tasks;

class CodeBlockKarelPickBeeper : CodeBlock
{
    public override string Name => "PickBeeper";

    protected override Task OnExecute(CodeBlockContext ctx)
        => ctx.KarelProgram.Robot.PickBeeper();
}
