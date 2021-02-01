
using System.Threading.Tasks;

public interface IReturnValue { }

public interface IReturnValue<T> : IReturnValue
{
    Task<T> GetValue(CodeBlockContext ctx);
}