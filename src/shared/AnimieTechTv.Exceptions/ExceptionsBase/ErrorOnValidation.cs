namespace AnimieTechTv.Exceptions.ExceptionsBase;

public class ErrorOnValidation : AnimieTechTVException
{
    public IList<string> ErrorMessage { get; set; }

    public ErrorOnValidation(IList<string> errorMessage) : base(string.Empty)
    {
        ErrorMessage = errorMessage;
    }

    public ErrorOnValidation(string errorMessage) : base(string.Empty)
    {
        ErrorMessage = new List<string> { errorMessage };
    }
}
