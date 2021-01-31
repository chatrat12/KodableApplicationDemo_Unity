
using System.Threading.Tasks;

public abstract class CodeBlockVariable
{

}

public class CodeBlockVariable<T> : CodeBlockVariable, IReturnValue<T>
{
    public T Value
    {
        get => _value;
        set => _value = value;
    }

    private T _value;

    public Task<T> GetValue() => Task.FromResult(_value);
}