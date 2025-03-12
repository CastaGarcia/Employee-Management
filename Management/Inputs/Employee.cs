namespace Management.Inputs
{
    public record EmployeeCreationData(string? Id, string FirstName, string LastName, int Dui);
        
    public record EmployeeUpdateData(string? Id, string FirstName, string LastName, int Dui);

    public record EmployeeGetFilter(string? NameContains, int Page, int ItemsPerPage);
   
}
