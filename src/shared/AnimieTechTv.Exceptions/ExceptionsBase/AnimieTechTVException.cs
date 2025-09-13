namespace AnimieTechTv.Exceptions.ExceptionsBase;

public class AnimieTechTVException : SystemException
{
    public AnimieTechTVException(string message) : base(message) { }

    public AnimieTechTVException(string message, Exception innerException) : base(message, innerException) { }
}
