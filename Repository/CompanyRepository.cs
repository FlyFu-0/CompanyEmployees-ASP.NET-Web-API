using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
	public CompanyRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
	{
	}

	public async Task<IEnumerable<Company>> GetAllCompaniesAsync(CompanyParametrs companyParametrs, bool trackChanges)
		=> await FindAll(trackChanges)
			.Sort(companyParametrs.OrderBy)
			.ToListAsync();

	public async Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges)
		=> await FindByCondition(c => c.Id.Equals(companyId), trackChanges)
			.SingleOrDefaultAsync();

	public void CreateCompanyAsync(Company company)
		=> Create(company);

	public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
		=> await FindByCondition(x => ids.Contains(x.Id), trackChanges)
			.ToListAsync();

	public void DeleteCompanyAsync(Company company)
		=> Delete(company);
}
