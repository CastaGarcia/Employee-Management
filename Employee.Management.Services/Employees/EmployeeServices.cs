﻿using Employees.Management.Data;
using Employees.Management.Models;
using Management;
using Management.Inputs;

namespace Employees.Management.Services.Employees
{
    public class EmployeeServices : IEmployeeServices
    {
        readonly IEmployeeRepo _employeeRepo;

        public EmployeeServices(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        public async Task<Employee?> Create(EmployeeCreationData employeeCreationData)
        {
            if (string.IsNullOrEmpty(employeeCreationData.Id) == false)
            {
                Employee? employeExist = await _employeeRepo.GetById(employeeCreationData.Id);
                if (employeExist != null)
                    throw new Exception("employe already exist");
            }

            Employee employe = new Employee(
                employeeCreationData.FirstName,
                employeeCreationData.LastName,
                employeeCreationData.Dui,
                employeeCreationData.Id
                );

            await _employeeRepo.AddAsync(employe);
            await _employeeRepo.SaveChangesAsync();
            return employe;
        }

        public async Task Delete(string employeeId)
        {
            var employee = await _employeeRepo.GetById(employeeId);

            if (employee != null)
            {
                await _employeeRepo.Delete(employeeId);  
                await _employeeRepo.SaveChangesAsync();  
            }
        }
        
        public async Task<Employee?> GetById(string id)
        {
            Employee? employeExist = await _employeeRepo.GetById(id);
            if (employeExist == null)
                throw new Exception("the employe that you look for doesnt eist");

            return employeExist;
        }
        public async Task<PaginatedListOutput<Employee>> GetEmployeesByFilter(EmployeeGetFilter employeeGetFilter)
        {
            var paginatedEmployees = await _employeeRepo.GetEmployeesByFilter(employeeGetFilter);


            return paginatedEmployees;
        }
      
        public async Task<Employee?> Update(EmployeeUpdateData employeeUpdateData)
        {
            if (string.IsNullOrEmpty(employeeUpdateData.Id))
                throw new Exception("the Id is empty");

            Employee? employeExist = await _employeeRepo.GetById(employeeUpdateData.Id);
            if (employeExist == null)
                throw new Exception("the employe that you look for doesnt eist");

            employeExist.FirstName = employeeUpdateData.FirstName;
            employeExist.LastName = employeeUpdateData.LastName;
            employeExist.Dui = employeeUpdateData.Dui;

            await _employeeRepo.SaveChangesAsync();
            return employeExist;
        }
    }
}
