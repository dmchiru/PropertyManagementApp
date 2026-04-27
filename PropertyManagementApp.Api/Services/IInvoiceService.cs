using PropertyManagementApp.Shared.DTOs;

namespace PropertyManagementApp.Api.Services
{
    public interface IInvoiceService
    {
        Task<List<InvoiceDto>> GetAllAsync();
        Task<InvoiceDto> CreateAsync(CreateInvoiceDto dto);
        Task<InvoiceDto> MarkPaidAsync(int invoiceId);
        Task<InvoiceDto> SendAsync(int invoiceId);
        Task<InvoiceDto> GenerateFromRentAsync(int scheduleId);
    }
}