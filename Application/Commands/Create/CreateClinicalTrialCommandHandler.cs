using Domain.Entities;
using Application.Interfaces;
using MediatR;
using AutoMapper;

namespace Application.Commands.Create;
public class CreateClinicalTrialCommandHandler : IRequestHandler<CreateClinicalTrialCommand, int>
{
    private readonly IClinicalTrialRepository _repository;
    private readonly IMapper _mapper;

    public CreateClinicalTrialCommandHandler(IClinicalTrialRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateClinicalTrialCommand request, CancellationToken cancellationToken)
    {
        var trial = _mapper.Map<ClinicalTrial>(request);

        trial.SetDefaultEndDateIfOngoing();
        trial.CalculateDuration();
        await _repository.AddAsync(trial, cancellationToken);

        return trial.Id;
    }
}
