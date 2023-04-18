using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.PatientAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

public class PatientRecordsContext : DbContext
{
    #pragma warning disable CS8618 // Required by Entity Framework

    public PatientRecordsContext(DbContextOptions<PatientRecordsContext> options) : base(options) { }

    public DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(ConfigurePatient);
    }

    // I prefer to separate out the configuration of the of DbSets so it's easier to isolate changes.
    // This could easily be moved to separate file as well
    private void ConfigurePatient(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(p  => p.Id);

        builder.Property(p => p.FirstName)
            .IsRequired();

        builder.Property(p => p.LastName)
            .IsRequired();

        builder.Property(p => p.DateOfBirth)
            .IsRequired();

        builder.Property(p => p.Gender)
            .IsRequired();
    }
}
