namespace Management.Inputs
{
    /// <summary>
    /// Create Employee
    /// </summary>
    /// <param name="FirstName"></param>
    /// <param name="LastName"></param>
    /// <param name="Dui"></param>
    public record EmployeeCreationData(string? Id, string FirstName, string LastName, int Dui);

    /// <summary>
    /// Update Employee
    /// </summary>
    /// <param name="FirstName"></param>
    /// <param name="LastName"></param>
    /// <param name="Dui"></param>
    public record EmployeeUpdateData(string? Id, string FirstName, string LastName, int Dui);
}
