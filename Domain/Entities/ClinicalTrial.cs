using Domain.Enums;

namespace Domain.Entities;

public class ClinicalTrial
{
    public int Id { get; set; }
    public string TrialId { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int Participants { get; set; }
    public TrialStatus Status { get; set; }
    public int DurationInDays { get; set; }

    public void CalculateDuration()
    {
        if (StartDate != null && EndDate != null)
        {
            DurationInDays = (EndDate.Value - StartDate).Days;
        }
    }

    public void SetDefaultEndDateIfOngoing()
    {
        if (Status == TrialStatus.Ongoing && !EndDate.HasValue)
        {
            EndDate = StartDate.AddMonths(1);
        }
    }
}

