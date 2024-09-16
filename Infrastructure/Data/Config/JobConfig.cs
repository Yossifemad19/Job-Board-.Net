using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    public class JobConfig : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder
                .Property(job => job.JobLevel)
                .HasConversion<string>();
                

            builder
                .HasOne(job => job.Company)
                .WithMany(company => company.Jobs)
                .HasForeignKey(job => job.CompanyId);

            builder.HasMany(job=>job.Candidates)
                .WithOne(Candidate=>Candidate.Job)
                .HasForeignKey(Candidate=>Candidate.JobId);
        }
    }
}
