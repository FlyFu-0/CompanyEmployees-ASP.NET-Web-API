using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
	private readonly IServiceManager _services;

	public CompaniesController(IServiceManager services)
		=> _services = services;

	[HttpGet]
	public IActionResult GetCompanies()
	{
		try
		{
			var companies = _services.CompanyService.GetAllCompanies(false);

			return Ok(companies);
		}
		catch
		{
			return StatusCode(500, "Internal server error");
		}
	}
}
