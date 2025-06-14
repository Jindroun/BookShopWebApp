using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuditController : ControllerBase
    {
        private readonly IAuditService auditService;

        public AuditController(IAuditService auditService)
        {
            this.auditService = auditService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditDisplay>>> GetLogs()
        {
            var logs = await auditService.GetLogs().ToListAsync();
            return Ok(logs);
        }

        [HttpGet("{entityType}/{entityId}")]
        public async Task<ActionResult<IEnumerable<AuditDisplay>>> GetLogs(int entityId, IAuditService.AuditedEntityName entityType)
        {
            var logs = await auditService.GetLogs(entityId, entityType).ToListAsync();
            return Ok(logs);
        }
    }
}
