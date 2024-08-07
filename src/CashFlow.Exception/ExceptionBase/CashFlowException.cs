using System.Security.Cryptography.X509Certificates;

namespace CashFlow.Exception.ExceptionBase;

public abstract class CashFlowException : SystemException
{
    public abstract int StatusCode { get; }
 
    public abstract List<string> GetErrors();

    protected CashFlowException(string message) : base(message) 
    {
    }
}
