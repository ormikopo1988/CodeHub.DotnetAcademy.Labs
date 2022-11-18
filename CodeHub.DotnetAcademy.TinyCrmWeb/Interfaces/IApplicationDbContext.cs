using CodeHub.DotnetAcademy.TinyCrmWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeHub.DotnetAcademy.TinyCrmWeb.Interfaces
{
	public interface IApplicationDbContext
	{
		public DbSet<Customer> Customers { set; get; }

		public DbSet<Product> Products { set; get; }

		Task<int> SaveChangesAsync();
	}
}
