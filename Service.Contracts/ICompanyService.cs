using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ICompanyService
{
	IEnumerable<CompanyDto> GetAllCompanies(bool tackChanges);

	CompanyDto GetCompany(Guid id, bool trackChanges);
}
