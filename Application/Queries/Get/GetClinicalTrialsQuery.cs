using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Queries.Get;

public class GetClinicalTrialsQuery : IRequest<IEnumerable<ClinicalTrialResponse>>
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
    public TrialStatus Status { get; set; }
}
