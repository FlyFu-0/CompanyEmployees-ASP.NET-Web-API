using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Employee
{
	[Column("EmployeeId")]
	public Guid Id { get; set; }
	public string? Name { get; set; }
	public int Age { get; set; }
	public string? Position { get; set; }

	[ForeignKey(nameof(Company))]
	public Guid CompanyId { get; set; }
	public Company? Company { get; set; }
}