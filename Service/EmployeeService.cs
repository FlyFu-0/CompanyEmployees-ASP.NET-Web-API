﻿using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

internal sealed class EmployeeService : IEmployeeService
{
	private readonly IRepositoryManager _repository;
	private readonly ILoggerManager _logger;
	private readonly IMapper _mapper;

	public EmployeeService(IRepositoryManager repository, ILoggerManager logger,
		IMapper mapper)
	{
		_repository = repository;
		_logger = logger;
		_mapper = mapper;
	}

	public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId,
		EmployeeParameters employeeParameters, bool trackChanges)
	{
		await CheckIfCompanyExist(companyId, trackChanges);

		var employeesFromDb = await _repository.Employee
			.GetEmployeesAsync(companyId, employeeParameters, trackChanges);

		var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);

		return employeesDto;
	}

	private async Task CheckIfCompanyExist(Guid companyId, bool trackChanges)
	{
		var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);

		if (company is null)
			throw new CompanyNotFoundException(companyId);
	}

	public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
	{
		await CheckIfCompanyExist(companyId, trackChanges);

		var employeeDb = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);

		var employeeDto = _mapper.Map<EmployeeDto>(employeeDb);

		return employeeDto;
	}

	private async Task<Employee> GetEmployeeForCompanyAndCheckIfItExists
		(Guid companyId, Guid id, bool trackChanges)
	{
		var employeeDb = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
		if (employeeDb is null)
			throw new EmployeeNotFoundException(id);

		return employeeDb;
	}

	public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges)
	{
		await CheckIfCompanyExist(companyId, trackChanges);

		var employeeEntity = _mapper.Map<Employee>(employeeForCreation);

		_repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
		await _repository.SaveAsync();

		var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

		return employeeToReturn;
	}

	public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges)
	{
		await CheckIfCompanyExist(companyId, trackChanges);

		var employeeDb = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);

		_repository.Employee.DeleteEmployee(employeeDb);
		await _repository.SaveAsync();
	}

	public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges)
	{
		await CheckIfCompanyExist(companyId, compTrackChanges);

		var employeeDb = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges);

		_mapper.Map(employeeForUpdate, employeeDb);
		await _repository.SaveAsync();
	}

	public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)>
		GetEmployeeForPatchAsync
			(Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges)
	{
		await CheckIfCompanyExist(companyId, compTrackChanges);

		var employeeDb = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges);


		var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeDb);

		return (employeeToPatch: employeeToPatch, employeeEntity: employeeDb);

	}

	public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
	{
		_mapper.Map(employeeToPatch, employeeEntity);
		await _repository.SaveAsync();
	}
}
