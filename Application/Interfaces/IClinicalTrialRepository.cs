using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces;
public interface IClinicalTrialRepository
{
    Task<ClinicalTrial> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<ClinicalTrial>> GetAllAsync(int skip, int take, TrialStatus? status, CancellationToken cancellationToken);
    Task AddAsync(ClinicalTrial clinicalTrial, CancellationToken cancellationToken);
    Task UpdateAsync(ClinicalTrial clinicalTrial, CancellationToken cancellationToken);
}
