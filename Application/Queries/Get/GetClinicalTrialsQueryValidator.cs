using FluentValidation;

namespace Application.Queries.Get;
public class GetClinicalTrialsQueryValidator : AbstractValidator<GetClinicalTrialsQuery>
{
    public GetClinicalTrialsQueryValidator()
    {
        RuleFor(x => x.Skip)
            .GreaterThanOrEqualTo(0).WithMessage("Skip cannot be less than 0.");

        RuleFor(x => x.Take)
            .GreaterThan(0).WithMessage("Take must be greater than 0.");
    }
}
