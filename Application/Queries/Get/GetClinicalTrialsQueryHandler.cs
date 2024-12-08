using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Queries.Get;

public class GetClinicalTrialsQueryHandler : IRequestHandler<GetClinicalTrialsQuery, IEnumerable<ClinicalTrialResponse>>
{
    private readonly IClinicalTrialRepository _repository;
    private readonly IMapper _mapper;

    public GetClinicalTrialsQueryHandler(IClinicalTrialRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClinicalTrialResponse>> Handle(GetClinicalTrialsQuery request, CancellationToken cancellationToken)
    {
        var trials = await _repository.GetAllAsync(request.Skip, request.Take, request.Status, cancellationToken);
        return _mapper.Map<IEnumerable<ClinicalTrialResponse>>(trials);
    }
}