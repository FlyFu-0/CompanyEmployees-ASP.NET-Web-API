using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private ILoggerManager _logger;

		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		public WeatherForecastController(ILoggerManager logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IEnumerable<string> Get()
		{
			_logger.LogInfo("Here is info messaege from our values controller");
			_logger.LogDebug("Here is debug messaege from our values controller");
			_logger.LogWarn("Here is warn messaege from our values controller");
			_logger.LogError("Here is error messaege from our values controller");

			return new string[] { "value1", "value2" };
		}
	}
}
