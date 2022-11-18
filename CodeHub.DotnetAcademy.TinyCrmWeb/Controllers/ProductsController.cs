using CodeHub.DotnetAcademy.TinyCrmWeb.Entities;
using CodeHub.DotnetAcademy.TinyCrmWeb.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CodeHub.DotnetAcademy.TinyCrmWeb.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IApplicationDbContext _applicationDbContext;
		private readonly ILogger<ProductsController> _logger;

		public ProductsController(IApplicationDbContext applicationDbContext, ILogger<ProductsController> logger)
		{
			_applicationDbContext = applicationDbContext;
			_logger = logger;
		}

		// GET: Products
		public async Task<IActionResult> Index()
		{
			var products = await _applicationDbContext.Products.ToListAsync();

			return View(products);
		}

		// GET: Products/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || id <= 0)
			{
				return NotFound();
			}

			var product = await _applicationDbContext
				.Products
				.SingleOrDefaultAsync(pro => pro.Id == id);

			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// GET: Products/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Products/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Code,Name,Description,Price,Quantity")] Product product)
		{
			if (ModelState.IsValid)
			{
				if (string.IsNullOrWhiteSpace(product.Code) ||
					string.IsNullOrWhiteSpace(product.Description) ||
					string.IsNullOrWhiteSpace(product.Name) ||
					product.Price <= 0 ||
					product.Quantity <= 0
				)
				{
					return BadRequest();
				}

				var productWithSameCode = await _applicationDbContext.Products.SingleOrDefaultAsync(pro => pro.Code == product.Code);

				if (productWithSameCode != null)
				{
					return BadRequest();
				}

				var newProduct = new Product
				{
					Name = product.Name,
					Price = product.Price,
					Code = product.Code,
					Description = product.Description,
					Quantity = product.Quantity
				};

				await _applicationDbContext.Products.AddAsync(newProduct);

				try
				{
					await _applicationDbContext.SaveChangesAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);
				}

				return RedirectToAction(nameof(Index));
			}

			return View(product);
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var products = await _applicationDbContext.Products.ToListAsync();

			return Ok(products);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			if (id <= 0)
			{
				return NotFound();
			}

			var productToDelete = await _applicationDbContext
				.Products
				.SingleOrDefaultAsync(pro => pro.Id == id);

			if (productToDelete == null)
			{
				return NotFound();
			}

			_applicationDbContext.Products.Remove(productToDelete);

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
