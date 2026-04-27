using Microsoft.EntityFrameworkCore;
using PropertyManagementApp.Api.Data;
using PropertyManagementApp.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRentScheduleService, RentScheduleService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();

builder.Services.AddScoped<IEvictionCaseService, EvictionCaseService>();

builder.Services.AddScoped<ICommunicationLogService, CommunicationLogService>();

builder.Services.AddScoped<DashboardService>();

builder.Services.AddDbContext<PropertyManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazor");

app.UseAuthorization();

app.MapControllers();

app.Run();