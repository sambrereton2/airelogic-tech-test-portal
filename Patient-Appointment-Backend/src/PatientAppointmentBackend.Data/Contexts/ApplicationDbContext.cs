using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PatientAppointmentBackend.Data.Entities;

namespace PatientAppointmentBackend.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var useSqlite = Configuration["UseSqlite"];
            //if (useSqlite != null)
            //{
            //    if(string.Equals(useSqlite, "true", StringComparison.OrdinalIgnoreCase))
            //    {
            //        optionsBuilder.UseSqlite(Configuration.GetConnectionString("PatientAppointmentDb"));
            //        return;
            //    }
            //}
            //// Use In-Memory db
            //optionsBuilder.UseInMemoryDatabase("TestDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().Navigation(e => e.Clinician).AutoInclude();
            modelBuilder.Entity<Appointment>().Navigation(e => e.Department).AutoInclude();
            modelBuilder.Entity<Appointment>().Navigation(e => e.Patient).AutoInclude();
            modelBuilder.Entity<Appointment>().Property(e => e.Status).HasConversion<int>();
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Clinician> Clinicians { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
    }
}
