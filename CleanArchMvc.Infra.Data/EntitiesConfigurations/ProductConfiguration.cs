using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.EntitiesConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(t => t.Id);
            //Name
            builder.Property(p=>p.Name)
                .HasMaxLength(100)
                .IsRequired();

            //Description
            builder.Property(p => p.Description)
                .HasMaxLength(200)
                .IsRequired();

            //Price
            builder.Property(p => p.Price)
                .HasPrecision(10, 2);

            //Category
            builder.HasOne(e => e.Category)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.CategoryId);

        }
    }
}
