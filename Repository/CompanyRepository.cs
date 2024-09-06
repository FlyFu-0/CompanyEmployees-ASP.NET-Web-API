using Contracts;
using Entities.Models;

namespace Repository;

public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
	public CompanyRepository(RepositoryContext repositoryContext)
		: base(repositoryContext)
	{
	}

	//TODO: Get all entities - bad idea
	public IEnumerable<Company> GetAllCompanies(bool trackChanges)
		=> FindAll(trackChanges)
			.OrderBy(c => c.Name)
			.ToList();
}
