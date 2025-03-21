﻿using Employees.Management.Models;
using Management;
using Management.Inputs;
using Management.Outputs;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Employees.Management.Data.Repos
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly AppDbContext _db;
        public EmployeeRepo(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Employee employee)
        {
            await _db.Employees.AddAsync(employee);
            await _db.SaveChangesAsync();

        }

        public async Task<Employee?> GetById(string id)
        {
            return await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Delete(string id)
        {
            var employee = await _db.Employees.FindAsync(id);

            if (employee == null)
            {
                throw new KeyNotFoundException("Empleado no encontrado");
            }

            _db.Employees.Remove(employee);

            await _db.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<Employee?> Update(Employee employee)
        {
            var existingEmployee = await _db.Employees.FindAsync(employee.Id);

            if (existingEmployee == null)
            {
                return null; 
            }
            existingEmployee.Id = employee.Id;
            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Dui = employee.Dui;
            
            _db.Employees.Update(existingEmployee);

            await SaveChangesAsync();

            return existingEmployee;
        }

        public async Task<PaginatedListOutput<Employee>> GetEmployeesByFilter(EmployeeGetFilter employeeGetFilter)
        {
            Expression<Func<Employee, bool>> where = x =>
                string.IsNullOrEmpty(employeeGetFilter.NameContains) || x.FirstName.ToLower().Contains(employeeGetFilter.NameContains.ToLower());

            int skip = employeeGetFilter.ItemsPerPage * (employeeGetFilter.Page - 1);

            var items = await _db.Employees
                .Where(where)
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(employeeGetFilter.ItemsPerPage)
                .ToListAsync();

            int totalItems = await _db.Employees.CountAsync(where);

            return new PaginatedListOutput<Employee>(items, totalItems, employeeGetFilter.Page, employeeGetFilter.ItemsPerPage);
        }
    }
}
