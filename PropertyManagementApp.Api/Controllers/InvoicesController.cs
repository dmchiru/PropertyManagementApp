using Microsoft.AspNetCore.Mvc;
using PropertyManagementApp.Api.Services;
using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _service;

        public InvoicesController(IInvoiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<InvoiceDto>>> GetInvoices()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceDto>> CreateInvoice(CreateInvoiceDto dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }

        [HttpPost("{id}/send")]
        public async Task<ActionResult<InvoiceDto>> SendInvoice(int id)
        {
            return Ok(await _service.SendAsync(id));
        }

        [HttpPost("{id}/mark-paid")]
        public async Task<ActionResult<InvoiceDto>> MarkPaid(int id)
        {
            return Ok(await _service.MarkPaidAsync(id));
        }
    }
}