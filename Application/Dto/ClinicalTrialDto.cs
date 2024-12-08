using Domain.Enums;

namespace Application.Dto;
public class ClinicalTrialDto
{
    public int Id { get; set; }
    public string TrialId { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int Participants { get; set; }
    public TrialStatus Status { get; set; }
    public int DurationInDays { get; set; }
}
