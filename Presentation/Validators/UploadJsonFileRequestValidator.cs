using Presentation.Requests;
using FluentValidation;

namespace Presentation.Validators;
public class UploadJsonFileRequestValidator : AbstractValidator<UploadJsonFileRequest>
{
    public UploadJsonFileRequestValidator()
    {
        RuleFor(x => x.File)
            .NotNull().WithMessage("File cannot be null.")
            .Must(f => f.FileName.EndsWith(".json")).WithMessage("Invalid file type. Only .json files are allowed.");
    }
}
