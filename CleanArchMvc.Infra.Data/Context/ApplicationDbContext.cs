using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> //DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly); // Este recurso ApplyConfigurations varre o projeto e
                                                                                            // vai lá na pasta EntitiesConfigurations e aplicação
                                                                                            // classe por classe que for do tipo IEntityTypeConfiguration e
                                                                                            // aplica, sem eu precisar setar um a um as configuraçoes.
        }
    }
}
