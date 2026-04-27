using Microsoft.EntityFrameworkCore;
using PropertyManagementApp.Api.Models;

namespace PropertyManagementApp.Api.Data
{
    public class PropertyManagementDbContext : DbContext
    {
        public PropertyManagementDbContext(DbContextOptions<PropertyManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<RentSchedule> RentSchedules { get; set; }
        public DbSet<RentPayment> RentPayments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<MaintenanceProject> MaintenanceProjects { get; set; }
        public DbSet<EvictionCase> EvictionCases { get; set; }
        public DbSet<CommunicationLog> CommunicationLogs { get; set; }
        public DbSet<WorkLog> WorkLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>().HasKey(p => p.PropertyID);
            modelBuilder.Entity<Tenant>().HasKey(t => t.TenantID);
            modelBuilder.Entity<RentSchedule>().HasKey(r => r.ScheduleID);
            modelBuilder.Entity<RentPayment>().HasKey(rp => rp.PaymentID);
            modelBuilder.Entity<RentSchedule>()
                .HasMany(r => r.RentPayments)
                .WithOne(p => p.RentSchedule)
                .HasForeignKey(p => p.ScheduleID);

            modelBuilder.Entity<Invoice>()
                .HasKey(i => i.InvoiceID);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.RentSchedule)
                .WithMany()
                .HasForeignKey(i => i.ScheduleID);

            modelBuilder.Entity<MaintenanceProject>()
                .HasKey(m => m.ProjectID);

            modelBuilder.Entity<MaintenanceProject>()
                .HasOne(m => m.Property)
                .WithMany()
                .HasForeignKey(m => m.PropertyID);

            modelBuilder.Entity<EvictionCase>()
                .HasKey(e => e.CaseID);

            modelBuilder.Entity<EvictionCase>()
                .HasOne(e => e.Tenant)
                .WithMany()
                .HasForeignKey(e => e.TenantID);

            modelBuilder.Entity<CommunicationLog>()
                .HasKey(c => c.LogID);

            modelBuilder.Entity<CommunicationLog>()
                .HasOne(c => c.Tenant)
                .WithMany()
                .HasForeignKey(c => c.TenantID);

            modelBuilder.Entity<CommunicationLog>()
                .HasOne(c => c.RentSchedule)
                .WithMany()
                .HasForeignKey(c => c.ScheduleID);

            modelBuilder.Entity<WorkLog>()
                .HasKey(w => w.LogID);

            modelBuilder.Entity<WorkLog>()
                .HasOne(w => w.MaintenanceProject)
                .WithMany()
                .HasForeignKey(w => w.ProjectID);
        }
    }
}