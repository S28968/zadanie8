namespace WebApplication2.Exceptions;

[Serializable]
public class ValidationException : Exception
{
    public ValidationException(string message)
        : base(message)
    {}
    
}