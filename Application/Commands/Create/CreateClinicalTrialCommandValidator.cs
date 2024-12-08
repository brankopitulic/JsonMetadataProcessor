using FluentValidation;

namespace Application.Commands.Create;

public class CreateClinicalTrialCommandValidator : AbstractValidator<CreateClinicalTrialCommand>
{
    public CreateClinicalTrialCommandValidator()
    {
        RuleFor(c => c.TrialId)
            .NotEmpty().WithMessage("TrialId is required.")
            .MaximumLength(50).WithMessage("TrialId cannot exceed 50 characters.");

        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(c => c.StartDate)
            .NotEmpty().WithMessage("StartDate is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("StartDate cannot be in the future.");

        RuleFor(c => c.Participants)
            .GreaterThan(0).WithMessage("Participants must be greater than zero.");

        RuleFor(c => c.Status)
            .IsInEnum().WithMessage("Invalid status. Allowed values are 'NotStarted', 'Ongoing', or 'Completed'.");
    }
}