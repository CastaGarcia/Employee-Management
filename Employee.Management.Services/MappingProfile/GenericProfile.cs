﻿using AutoMapper;
using Employees.Management.Models;
using Management;
using Management.Outputs;

namespace Employees.Management.Services.MappingProfile
{
    public class GenericProfile : Profile
    {
        public GenericProfile()
        {
            CreateMap<Employee, EmployeeOutput>();
            CreateMap<User, UserOutput>();


            CreateMap<PaginatedListOutput<Employee>, PaginatedListOutput<EmployeeOutput>>();
        }
            
    }
}
