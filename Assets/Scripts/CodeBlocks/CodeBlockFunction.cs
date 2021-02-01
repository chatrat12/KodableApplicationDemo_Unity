using System.Threading.Tasks;

public class CodeBlockFunction : CodeBlockBody
{
}

public class CodeBlockFunction<T> : CodeBlockFunction, IReturnValue<T>
{
    private T _returnValue = default(T);

    public async Task<T> GetValue(CodeBlockContext ctx)
    {
        await Execute(ctx);
        return _returnValue;
    }
}
