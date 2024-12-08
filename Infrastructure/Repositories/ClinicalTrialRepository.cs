using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Repositories;
public class ClinicalTrialRepository : IClinicalTrialRepository
{
    private readonly ApplicationDbContext _context;

    public ClinicalTrialRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ClinicalTrial> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        Log.Information("Fetching clinical trial with ID: {Id}", id);

        var item = await _context.ClinicalTrials.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (item == null)
        {
            Log.Warning("Clinical trial with ID {Id} not found", id);
            throw new ArgumentException($"Clinical trial with ID {id} not found");
        }

        Log.Information("Successfully fetched clinical trial with ID: {Id}", id);
        return item;
    }

    public async Task<IEnumerable<ClinicalTrial>> GetAllAsync(int skip = 0, int take = 10, TrialStatus? status = default, CancellationToken cancellationToken = default)
    {
        Log.Information("Fetching clinical trials with pagination: Skip={Skip}, Take={Take}, Status={Status}", skip, take, status);

        var query = _context.ClinicalTrials.AsNoTracking();

        if (status.HasValue)
        {
            Log.Information("Applying filter: Status={Status}", status);
            query = query.Where(x => x.Status == status.Value);
        }

        var trials = await query.Skip(skip).Take(take).ToListAsync(cancellationToken);

        Log.Information("Successfully fetched {Count} clinical trials", trials.Count);
        return trials;
    }

    public async Task AddAsync(ClinicalTrial clinicalTrial, CancellationToken cancellationToken = default)
    {
        Log.Information("Adding a new clinical trial: {@ClinicalTrial}", clinicalTrial);

        try
        {
            _context.ClinicalTrials.Add(clinicalTrial);
            await _context.SaveChangesAsync(cancellationToken);
            Log.Information("Successfully added clinical trial with ID: {Id}", clinicalTrial.Id);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error occurred while adding clinical trial: {@ClinicalTrial}", clinicalTrial);
            throw;
        }
    }

    public async Task UpdateAsync(ClinicalTrial clinicalTrial, CancellationToken cancellationToken = default)
    {
        Log.Information("Updating clinical trial with ID: {Id}", clinicalTrial.Id);

        try
        {
            _context.ClinicalTrials.Update(clinicalTrial);
            await _context.SaveChangesAsync(cancellationToken);
            Log.Information("Successfully updated clinical trial with ID: {Id}", clinicalTrial.Id);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error occurred while updating clinical trial: {@ClinicalTrial}", clinicalTrial);
            throw;
        }
    }
}
