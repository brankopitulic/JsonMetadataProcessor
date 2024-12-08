using Domain.Enums;
using MediatR;

namespace Application.Commands.Create;
public class CreateClinicalTrialCommand : IRequest<int>
{
    public string TrialId { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int Participants { get; set; }
    public TrialStatus Status { get; set; }
}
