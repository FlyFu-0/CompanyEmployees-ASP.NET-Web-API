﻿using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface ICompanyService
{
	Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(CompanyParametrs companyParametrs, bool trackChanges);

	Task<CompanyDto> GetCompanyAsync(Guid id, bool trackChanges);

	Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto company);

	Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);

	Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync
		(IEnumerable<CompanyForCreationDto> companyCollection);

	Task DeleteCompanyAsync(Guid companyId, bool trackChanges);

	Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdate,
		bool trackChanges);
}
