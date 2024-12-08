using MediatR;

namespace Application.Commands.Validation;
public class ValidateJsonSchemaCommand : IRequest<ValidationResult>
{
    public string JsonContent { get; set; }
}

public class ValidationResult
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; }
}
