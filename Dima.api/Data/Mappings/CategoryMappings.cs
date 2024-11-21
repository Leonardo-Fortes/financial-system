using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
//Configuração para o Migrations

namespace Dima.api.Data.Mappings
{
    public class CategoryMappings : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired(true)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Description).IsRequired(false)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(220);

            builder.Property(x => x.UserID).IsRequired(true)
                .HasColumnType("VARCHAR")
                .HasMaxLength(160);
        }
    }
}
