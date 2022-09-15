using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<GithubProfile> GithubProfiles { get; set; }

        //Auth
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>(l =>
            {
                l.ToTable("Languages").HasKey(k => k.Id);
                l.Property(p => p.Id).HasColumnName("Id");
                l.Property(p => p.Name).HasColumnName("Name");
                l.Property(p => p.Version).HasColumnName("Version");
                l.HasMany(p => p.Technologies);
            });


            modelBuilder.Entity<Technology>(t =>
            {
                t.ToTable("Technologies").HasKey(k => k.Id);
                t.Property(p => p.Id).HasColumnName("Id");
                t.Property(p => p.LanguageId).HasColumnName("LanguageId");
                t.Property(p => p.Name).HasColumnName("Name");
                t.Property(p => p.Version).HasColumnName("Version");
                t.HasOne(p => p.Language);
            });


            modelBuilder.Entity<GithubProfile>(g =>
            {
                g.ToTable("GithubProfiles").HasKey(k => k.Id);
                g.Property(p => p.Id).HasColumnName("Id");
                g.Property(p => p.UserId).HasColumnName("UserId");
                g.Property(p => p.RepoName).HasColumnName("RepoName");
                g.Property(p => p.RepoUrl).HasColumnName("RepoUrl");
            });


            modelBuilder.Entity<User>(u => {
                u.ToTable("Users").HasKey(k => k.Id);
                u.Property(p => p.Id).HasColumnName("Id");
                u.Property(p => p.FirstName).HasColumnName("FirstName");
                u.Property(p => p.LastName).HasColumnName("LastName");
                u.Property(p => p.Email).HasColumnName("Email");
                u.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
                u.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
                u.Property(p => p.Status).HasColumnName("Status");
                u.Property(p => p.AuthenticatorType).HasColumnName("AuthenticatorType");

                u.HasMany(p => p.UserOperationClaims);
                u.HasMany(p => p.RefreshTokens);
            });


            modelBuilder.Entity<OperationClaim>(a =>
            {
                a.ToTable("OperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(c => c.Name).HasColumnName("Name");
            });


            modelBuilder.Entity<UserOperationClaim>(a =>
            {
                a.ToTable("UserOperationClaims").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(c => c.UserId).HasColumnName("UserId");
                a.Property(c => c.OperationClaimId).HasColumnName("OperationClaimId");

                a.HasOne(c => c.OperationClaim);
                a.HasOne(c => c.User);
            });


            Language[] LanguagesEntitySeeds = { new(1, "Python", "3.15"), new(2, "C#", "10"), new(3, "Java", "18"), new(4, "Javascript", "ES6") };
            modelBuilder.Entity<Language>().HasData(LanguagesEntitySeeds);


            Technology[] tecnologyEntitySeeds = { new(1, 1, "Django", "4.1.1"), new(2, 2, "WPF", "4.6"), new(3, 3, "Spring", "5.3.22"), new(4, 4, "Angular", "18.2.0") };
            modelBuilder.Entity<Technology>().HasData(tecnologyEntitySeeds);


            OperationClaim[] operationClaimsEntitySeeds = { new(1, "admin"), new(2, "user"), new(3, "add,get,update") };
            modelBuilder.Entity<OperationClaim>().HasData(operationClaimsEntitySeeds);



        }

    }
}
