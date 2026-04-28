using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyManagementApp.Api.Data;
using PropertyManagementApp.Api.Models;
using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkLogsController : ControllerBase
    {
        private readonly PropertyManagementDbContext _context;

        public WorkLogsController(PropertyManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkLogDto>>> GetByProject([FromQuery] int projectId)
        {
            var logs = await _context.WorkLogs
                .Where(w => w.ProjectID == projectId)
                .OrderByDescending(w => w.ClockInTime)
                .Select(w => new WorkLogDto
                {
                    LogID = w.LogID,
                    ProjectID = w.ProjectID,
                    ClockInTime = w.ClockInTime,
                    ClockOutTime = w.ClockOutTime,
                    GPSLocation = w.GPSLocation,
                    ProofPhotoURL = w.ProofPhotoURL,
                    MaterialsUsed = w.MaterialsUsed,
                    VendorSignature = w.VendorSignature
                })
                .ToListAsync();

            return Ok(logs);
        }

        [HttpPost]
        public async Task<ActionResult<WorkLogDto>> Create(CreateWorkLogDto dto)
        {
            var log = new WorkLog
            {
                ProjectID = dto.ProjectID,
                ClockInTime = dto.ClockInTime,
                ClockOutTime = dto.ClockOutTime,
                GPSLocation = dto.GPSLocation,
                ProofPhotoURL = dto.ProofPhotoURL,
                MaterialsUsed = dto.MaterialsUsed,
                VendorSignature = dto.VendorSignature
            };

            _context.WorkLogs.Add(log);
            await _context.SaveChangesAsync();

            var result = new WorkLogDto
            {
                LogID = log.LogID,
                ProjectID = log.ProjectID,
                ClockInTime = log.ClockInTime,
                ClockOutTime = log.ClockOutTime,
                GPSLocation = log.GPSLocation,
                ProofPhotoURL = log.ProofPhotoURL,
                MaterialsUsed = log.MaterialsUsed,
                VendorSignature = log.VendorSignature
            };

            return Ok(result);
        }
    }
}