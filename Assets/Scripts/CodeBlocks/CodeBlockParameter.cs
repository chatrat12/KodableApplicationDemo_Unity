using System.Threading.Tasks;

public class CodeBlockParameter
{
}

public class CodeBlockParameter<T> : CodeBlockParameter
{
    private T _value;
    private IReturnValue<T> _referenceValue;

    public async Task<T> GetValue() 
        => _referenceValue != null ? await _referenceValue.GetValue() : _value;
}