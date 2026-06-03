using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AIResumeAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIResumeAnalyzer.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Resume> Resumes => Set<Resume>();

        public DbSet<ResumeAnalysis> ResumeAnalyses => Set<ResumeAnalysis>();

        public DbSet<JobDescription> JobDescriptions => Set<JobDescription>();

        public DbSet<JobMatch> JobMatches => Set<JobMatch>();

        public DbSet<InterviewSession> InterviewSessions => Set<InterviewSession>();

        public DbSet<InterviewQuestion> InterviewQuestions => Set<InterviewQuestion>();

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);

            modelBuilder.Entity<User>()
                .HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
