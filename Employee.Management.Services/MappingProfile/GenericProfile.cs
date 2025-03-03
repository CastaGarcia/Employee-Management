﻿using AutoMapper;
using Management.Outputs;

namespace Employees.Management.Services.MappingProfile
{
    public class GenericProfile : Profile
    {
        public GenericProfile()
        {
            CreateMap<Employee, EmployeeOutput>();
            CreateMap<User, UserOutput>();
        }
            
    }
}
