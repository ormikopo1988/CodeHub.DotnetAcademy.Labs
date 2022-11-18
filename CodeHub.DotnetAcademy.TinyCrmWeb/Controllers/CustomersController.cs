using CodeHub.DotnetAcademy.TinyCrmWeb.Entities;
using CodeHub.DotnetAcademy.TinyCrmWeb.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeHub.DotnetAcademy.TinyCrmWeb.Controllers
{
	public class CustomersController : Controller
	{
		private readonly IApplicationDbContext _applicationDbContext;
		private readonly ILogger<CustomersController> _logger;

		public CustomersController(IApplicationDbContext applicationDbContext, ILogger<CustomersController> logger)
		{
			_applicationDbContext = applicationDbContext;
			_logger = logger;
		}

		// GET: Customers
		public async Task<IActionResult> Index()
		{
			var customers = await _applicationDbContext.Customers.ToListAsync();

			return View(customers);
		}

		// GET: Customers/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || id <= 0)
			{
				return NotFound();
			}

			var customer = await _applicationDbContext
				.Customers
				.SingleOrDefaultAsync(cus => cus.Id == id);

			if (customer == null)
			{
				return NotFound();
			}

			return View(customer);
		}

		// GET: Customers/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Customers/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,VatNumber,Address")] Customer customer)
		{
			if (ModelState.IsValid)
			{
				if (string.IsNullOrWhiteSpace(customer.FirstName) ||
				  string.IsNullOrWhiteSpace(customer.LastName) ||
				  string.IsNullOrWhiteSpace(customer.Address) ||
				  string.IsNullOrWhiteSpace(customer.VatNumber))
				{
					return BadRequest();
				}

				if (customer.VatNumber.Length > 9)
				{
					return BadRequest();
				}

				var customerWithSameVat = await _applicationDbContext.Customers.SingleOrDefaultAsync(cus => cus.VatNumber == customer.VatNumber);

				if (customerWithSameVat != null)
				{
					return BadRequest();
				}

				var newCustomer = new Customer
				{
					VatNumber = customer.VatNumber,
					FirstName = customer.FirstName,
					LastName = customer.LastName,
					Address = customer.Address
				};

				await _applicationDbContext.Customers.AddAsync(newCustomer);

				try
				{
					await _applicationDbContext.SaveChangesAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);

					return BadRequest();
				}

				return RedirectToAction(nameof(Index));
			}

			return View(customer);
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var customers = await _applicationDbContext.Customers.ToListAsync();

			return Ok(customers);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			var customerToDelete = await _applicationDbContext
				.Customers
				.SingleOrDefaultAsync(cus => cus.Id == id);

			if (customerToDelete == null)
			{
				return NotFound();
			}

			_applicationDbContext.Customers.Remove(customerToDelete);

			try
			{
				await _applicationDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
			}

			return NoContent();
		}
	}
}
