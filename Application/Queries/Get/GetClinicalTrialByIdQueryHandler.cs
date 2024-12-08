using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.Queries.Get;
public class GetClinicalTrialByIdQueryHandler : IRequestHandler<GetClinicalTrialByIdQuery, ClinicalTrialDto>
{
    private readonly IClinicalTrialRepository _repository;

    public GetClinicalTrialByIdQueryHandler(IClinicalTrialRepository repository)
    {
        _repository = repository;
    }

    public async Task<ClinicalTrialDto> Handle(GetClinicalTrialByIdQuery request, CancellationToken cancellationToken)
    {
        var trial = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (trial == null)
        {
            throw new KeyNotFoundException($"Clinical trial with ID {request.Id} was not found.");
        }

        return new ClinicalTrialDto
        {
            Id = trial.Id,
            TrialId = trial.TrialId,
            Title = trial.Title,
            StartDate = trial.StartDate,
            EndDate = trial.EndDate,
            Participants = trial.Participants,
            Status = trial.Status,
            DurationInDays = trial.DurationInDays
        };
    }
}
