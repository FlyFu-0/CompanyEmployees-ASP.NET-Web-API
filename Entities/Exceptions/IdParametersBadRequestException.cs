namespace Entities.Exceptions;

public sealed class IdParametersBadRequestException : BadRequestException
{
	public IdParametersBadRequestException() : base("Parametr ids is null.")
	{
	}
}
