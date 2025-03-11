using Management.Inputs;
using Management.Outputs;
using Refit;

namespace Management
{
    public interface IEmployeeSdk
    {
        private const string BASEURL = "/api/employees";

        
        [Post(BASEURL)]
        Task<EmployeeOutput> Create([Body] EmployeeCreationData employeeCreationData);

        
        [Get(BASEURL + "{Id}")]
        Task<EmployeeOutput> GetEmployee(string id);

        [Get(BASEURL)]
        Task<PaginatedListOutput<EmployeeOutput>> GetEmployeesByFilter([Query] EmployeeGetFilter employeeGetFilter);


        [Delete(BASEURL + "{Id}")]
        Task<EmployeeOutput> DeleteEmployee(string id);

        
        [Put(BASEURL)]
        Task<EmployeeOutput> Update([Body] EmployeeUpdateData employeeUpdateData);
    }

}
