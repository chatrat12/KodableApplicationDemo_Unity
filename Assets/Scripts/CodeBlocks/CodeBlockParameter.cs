using System.Threading.Tasks;

public class CodeBlockParameter
{
}

public class CodeBlockParameter<T> : CodeBlockParameter
{
    private T _value;
    private IReturnValue<T> _referenceValue;

    public async Task<T> GetValue(CodeBlockContext ctx) 
        => _referenceValue != null ? await _referenceValue.GetValue(ctx) : _value;
}