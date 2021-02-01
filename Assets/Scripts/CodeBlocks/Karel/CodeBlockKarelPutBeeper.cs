using System.Threading.Tasks;

class CodeBlockKarelPutBeeper : CodeBlock
{
    public override string Name => "PutBeeper";

    protected override Task OnExecute(CodeBlockContext ctx)
        => ctx.KarelProgram.Robot.PutBeeper();
}