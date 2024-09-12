//using Contracts;
//using Entities.LinkModels;
//using Entities.Models;
//using Microsoft.Net.Http.Headers;
//using Shared.DataTransferObjects;

//namespace CompanyEmployees.Utility;

//public class EmployeeLinks : IEmployeeLinks
//{
//	private readonly LinkGenerator _linkGenerator;
//	private readonly IDataShaper<EmployeeDto> _dataShaper;

//	public EmployeeLinks(LinkGenerator linkGenerator, IDataShaper<EmployeeDto> dataShaper)
//	{
//		_linkGenerator = linkGenerator;
//		_dataShaper = dataShaper;
//	}

//	public LinkResponse TryGenerateLinks(IEnumerable<EmployeeDto> employeeDto, string fields,
//		Guid companyId, HttpContext httpContext)
//	{
//		var shapedEmployees = ShapeData(employeeDto, fields);

//		if (ShouldGenerateLinks(httpContext))
//			return ReturnLinkdedEmployees(employeeDto, fields, 
//				companyId, httpContext, shapedEmployees);

//		return ReturnShapedEmployees(shapedEmployees);
//	}

//	private LinkResponse ReturnShapedEmployees(List<Entity> shapedEmployees)
//		=> new LinkResponse { ShapedEntity = shapedEmployees };

//	private LinkResponse ReturnLinkdedEmployees(IEnumerable<EmployeeDto> employeeDto,
//		string fields, Guid companyId, HttpContext httpContext, List<Entity> shapedEmployees)
//	{
//		var employeeDtoList = employeeDto.ToList();

//		for (int index = 0; index < employeeDtoList.Count(); index++)
//		{
//			var employeeLinks = CreateLinksForEmployee(httpContext, companyId,
//				employeeDtoList[index].Id, fields);
//			shapedEmployees[index].Add("Links", employeeLinks);
//		}

//		var emploeeCollection = new LinkCollectionWrapper<Entity>(shapedEmployees);
//		var linkedEmployees = CreateLinksForEmployees(httpContext, emploeeCollection);

//		return new LinkResponse { HasLinks = true, LinkedEntities = linkedEmployees };
//	}

//	private LinkCollectionWrapper<Entity> CreateLinksForEmployees(HttpContext httpContext,
//		LinkCollectionWrapper<Entity> emploeeWrapper)
//	{
//		emploeeWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext,
//			"GetEmployeesForCompany", values: new { }),
//			"self",
//			"GET"));

//		return emploeeWrapper;
//	}

//	private List<Link> CreateLinksForEmployee(HttpContext httpContext,
//		Guid companyId, Guid id, string fields = "")
//	{
//		var links = new List<Link>
//		{
//			new Link(_linkGenerator.GetUriByAction(httpContext, "GetEmployeeFroCompany",
//			values: new { companyId, id, fields}),
//			"self",
//			"GET"),
//			new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteEmployeeFroCompany",
//			values: new { companyId, id}),
//			"delete_employee",
//			"DELETE"),
//			new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateEmployeeFroCompany",
//			values: new { companyId, id}),
//			"update_employee",
//			"PUT"),
//			new Link(_linkGenerator.GetUriByAction(httpContext, "PartiallyUpdateEmployeeFroCompany",
//			values: new { companyId, id, fields}),
//			"partially_update_employee",
//			"PATCH")
//		};

//		return links;
//	}

//	private bool ShouldGenerateLinks(HttpContext httpContext)
//	{
//		var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];

//        Console.WriteLine($"{mediaType}=========================================================");

//        return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
//	}

//	private List<Entity> ShapeData(IEnumerable<EmployeeDto> employeeDto, string fields)
//		=> _dataShaper.ShapeData(employeeDto, fields)
//				.Select(e => e.Entity)
//				.ToList();
//}


using Contracts;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Utility;

public class EmployeeLinks : IEmployeeLinks
{
	private readonly LinkGenerator _linkGenerator;
	private readonly IDataShaper<EmployeeDto> _dataShaper;

	public Dictionary<string, MediaTypeHeaderValue> AcceptHeader { get; set; } =
		new Dictionary<string, MediaTypeHeaderValue>();

	public EmployeeLinks(LinkGenerator linkGenerator, IDataShaper<EmployeeDto> dataShaper)
	{
		_linkGenerator = linkGenerator;
		_dataShaper = dataShaper;
	}

	public LinkResponse TryGenerateLinks(IEnumerable<EmployeeDto> employeesDto, string fields, Guid companyId,
		HttpContext httpContext)
	{
		var shapedEmployees = ShapeData(employeesDto, fields);

		if (ShouldGenerateLinks(httpContext))
			return ReturnLinkedEmployees(employeesDto, fields, companyId, httpContext, shapedEmployees);

		return ReturnShapedEmployees(shapedEmployees);
	}

	private List<Entity> ShapeData(IEnumerable<EmployeeDto> employeesDto, string fields) =>
		_dataShaper.ShapeData(employeesDto, fields)
			.Select(e => e.Entity)
			.ToList();

	private bool ShouldGenerateLinks(HttpContext httpContext)
	{
		var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];

		return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
	}

	private LinkResponse ReturnShapedEmployees(List<Entity> shapedEmployees)
		=> new LinkResponse { ShapedEntities = shapedEmployees };

	private LinkResponse ReturnLinkedEmployees(IEnumerable<EmployeeDto> employeesDto,
		string fields, Guid companyId, HttpContext httpContext, List<Entity> shapedEmployees)
	{
		var EmployeeDtoList = employeesDto.ToList();

		for (var index = 0; index < EmployeeDtoList.Count(); index++)
		{
			var employeeLinks = CreateLinksForEmployee(httpContext, companyId, EmployeeDtoList[index].Id, fields);
			shapedEmployees[index].Add("Links", employeeLinks);
		}

		var employeeCollection = new LinkCollectionWrapper<Entity>(shapedEmployees);
		var linkedEmployees = CreateLinksForEmployees(httpContext, employeeCollection);

		return new LinkResponse { HasLinks = true, LinkedEntities = linkedEmployees };
	}

	private List<Link> CreateLinksForEmployee(HttpContext httpContext, Guid companyId, Guid id, string fields = "")
	{
		var links = new List<Link>
			{
				new Link(_linkGenerator.GetUriByAction(httpContext, "GetEmployeeForCompany", values: new { companyId, id, fields }),
				"self",
				"GET"),
				new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteEmployeeForCompany", values: new { companyId, id }),
				"delete_employee",
				"DELETE"),
				new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateEmployeeForCompany", values: new { companyId, id }),
				"update_employee",
				"PUT"),
				new Link(_linkGenerator.GetUriByAction(httpContext, "PartiallyUpdateEmployeeForCompany", values: new { companyId, id }),
				"partially_update_employee",
				"PATCH")
			};
		return links;
	}

	private LinkCollectionWrapper<Entity> CreateLinksForEmployees(HttpContext httpContext,
		LinkCollectionWrapper<Entity> employeesWrapper)
	{
		employeesWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetEmployeesForCompany", values: new { }),
				"self",
				"GET"));

		return employeesWrapper;
	}
}