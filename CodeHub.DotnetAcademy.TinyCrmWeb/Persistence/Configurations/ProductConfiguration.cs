using CodeHub.DotnetAcademy.TinyCrmWeb.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CodeHub.DotnetAcademy.TinyCrmWeb.Persistence.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable("Product");
			builder.Property(pro => pro.Code)
				.IsRequired()
				.HasMaxLength(10);
			builder.Property(bd => bd.Description)
				.IsRequired()
				.HasMaxLength(500);
			builder.Property(bd => bd.Name)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(bd => bd.Price)
				.IsRequired();
			builder.Property(bd => bd.Quantity)
				.IsRequired();
		}
	}
}
