using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
	public CompanyRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
	{
	}

	//TODO: Get all entities - bad idea
	public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges)
		=> await FindAll(trackChanges)
			.OrderBy(c => c.Name)
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
