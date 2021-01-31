using System.Threading.Tasks;

public abstract class CodeBlock
{
    public abstract string Name { get; }

    public Task Execute() => OnExecute();

    protected abstract Task OnExecute();

}