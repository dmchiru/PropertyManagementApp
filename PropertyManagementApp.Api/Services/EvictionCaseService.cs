using Microsoft.EntityFrameworkCore;
using PropertyManagementApp.Api.Data;
using PropertyManagementApp.Api.Models;
using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Services
{
    public class EvictionCaseService : IEvictionCaseService
    {
        private readonly PropertyManagementDbContext _context;

        public EvictionCaseService(PropertyManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<EvictionCaseDto>> GetAllAsync()
        {
            return await _context.EvictionCases
                .Include(e => e.Tenant)
                    .ThenInclude(t => t!.Property)
                .OrderByDescending(e => e.OpenedAt)
                .Select(e => new EvictionCaseDto
                {
                    CaseID = e.CaseID,
                    TenantID = e.TenantID,
                    TenantName = e.Tenant != null
                        ? e.Tenant.FirstName + " " + e.Tenant.LastName
                        : "",
                    PropertyName = e.Tenant != null && e.Tenant.Property != null
                        ? e.Tenant.Property.PropertyName
                        : "",
                    UnitNumber = e.Tenant != null && e.Tenant.Property != null
                        ? e.Tenant.Property.UnitNumber
                        : "",
                    OpenedAt = e.OpenedAt,
                    Reason = e.Reason,
                    CurrentStep = e.CurrentStep,
                    Status = e.Status,
                    ClosedAt = e.ClosedAt,
                    Notes = e.Notes
                })
                .ToListAsync();
        }

        public async Task<EvictionCaseDto> CreateAsync(CreateEvictionCaseDto dto)
        {
            var evictionCase = new EvictionCase
            {
                TenantID = dto.TenantID,
                Reason = dto.Reason,
                Notes = dto.Notes,
                CurrentStep = "Prepare Notice",
                Status = "Active",
                OpenedAt = DateTime.Now
            };

            _context.EvictionCases.Add(evictionCase);
            await _context.SaveChangesAsync();

            return (await GetAllAsync()).First(x => x.CaseID == evictionCase.CaseID);
        }

        public async Task<EvictionCaseDto> GenerateNoticeAsync(int caseId)
        {
            var evictionCase = await _context.EvictionCases.FindAsync(caseId);

            if (evictionCase == null)
                throw new KeyNotFoundException("Eviction case not found.");

            evictionCase.CurrentStep = "Notice Generated";
            evictionCase.Notes = (evictionCase.Notes ?? "") +
                $"\nNotice generated on {DateTime.Now:g}.";

            await _context.SaveChangesAsync();

            return (await GetAllAsync()).First(x => x.CaseID == caseId);
        }

        public async Task<EvictionCaseDto> CloseAsync(int caseId)
        {
            var evictionCase = await _context.EvictionCases.FindAsync(caseId);

            if (evictionCase == null)
                throw new KeyNotFoundException("Eviction case not found.");

            evictionCase.Status = "Closed";
            evictionCase.CurrentStep = "Closed";
            evictionCase.ClosedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return (await GetAllAsync()).First(x => x.CaseID == caseId);
        }
    }
}