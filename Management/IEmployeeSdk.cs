using Management.Inputs;
using Management.Outputs;
using Refit;

namespace Management
{
    public interface IEmployeeSdk
    {
        private const string BASEURL = "api/employees";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeCreationData"></param>
        /// <returns></returns>
        [Post(BASEURL)]
        Task<EmployeeOutput> Create([Body] EmployeeCreationData employeeCreationData);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Get(BASEURL + "{Id}")]
        Task<EmployeeOutput> GetEmployee(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Delete(BASEURL + "{Id}")]
        Task<EmployeeOutput> DeleteEmployee(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeUpdateData"></param>
        /// <returns></returns>
        [Put(BASEURL)]
        Task<EmployeeOutput> Put([Body] EmployeeUpdateData employeeUpdateData);
    }

}
