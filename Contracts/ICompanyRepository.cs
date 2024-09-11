using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface ICompanyRepository
{
	Task<IEnumerable<Company>> GetAllCompaniesAsync(CompanyParametrs companyParametrs, bool trackChanges);

	Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges);

	void CreateCompanyAsync(Company company);

	Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);

	void DeleteCompanyAsync(Company company);
}
