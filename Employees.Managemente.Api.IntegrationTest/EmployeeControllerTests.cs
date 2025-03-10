using Management;
using Management.Inputs;
using Management.Outputs;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

public class EmployeeTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public EmployeeTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    async Task<HttpResponseMessage> createEmployee()
    {
        EmployeeCreationData newEmployee = new EmployeeCreationData(
            Id: Guid.NewGuid().ToString(),
            FirstName: "Maria",
            LastName: "Dolores",
            Dui: 87645521);

        HttpClient client = _factory.CreateClient();
        var employe = new StringContent(JsonConvert.SerializeObject(newEmployee), Encoding.UTF8, "application/json");

        return await client.PostAsync("api/employees", employe);
    }

    [Fact]
    public async Task CreateEmployee_Returns200()
    {
        HttpResponseMessage response = await createEmployee();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        EmployeeOutput? employee = JsonConvert.DeserializeObject<EmployeeOutput>(responseContent);

        Assert.NotNull(employee);
        Assert.NotNull(employee.Id);
    }

    [Fact]
    public async Task GetEmployeesByFilterReturnsPaginatedEmployees()
    {
        var filters = new EmployeeGetFilter(
            NameContains: "Maria",
            Page: 1,
            ItemsPerPage: 5
        );

        HttpClient client = _factory.CreateClient();

        var response = await client.GetAsync($"api/employees?NameContains={filters.NameContains}&Page={filters.Page}&ItemsPerPage={filters.ItemsPerPage}");

        Assert.True(response.IsSuccessStatusCode, $"Expected OK status but got {response.StatusCode}");

        var responseContent = await response.Content.ReadAsStringAsync();
        PaginatedListOutput<EmployeeOutput>? employeesResult = JsonConvert.DeserializeObject<PaginatedListOutput<EmployeeOutput>>(responseContent);

        Assert.NotNull(employeesResult);
        Assert.NotEmpty(employeesResult.Items);

        foreach (var employee in employeesResult.Items)
        {
            Assert.Contains(filters.NameContains, employee.FirstName);
        }

        Assert.True(employeesResult.TotalItemCount >= employeesResult.Items.Count);
    }




    [Fact]
    public async Task GetEmployee_Returns200()
    {
        HttpResponseMessage responseCreated = await createEmployee();

        var responseCreatedContent = await responseCreated.Content.ReadAsStringAsync();
        EmployeeOutput? employee = JsonConvert.DeserializeObject<EmployeeOutput>(responseCreatedContent);

        HttpClient client = _factory.CreateClient();
        var response = await client.GetAsync($"api/employees/{employee?.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var employee2 = JsonConvert.DeserializeObject<EmployeeOutput>(responseContent);

        Assert.NotNull(employee);
        Assert.Equal(employee2?.Id, employee.Id);
    }

    [Fact]
    public async Task UpdateEmployee_Returns200()
    {
        HttpResponseMessage responseSuccess = await createEmployee();

        var responseCreatedContent = await responseSuccess.Content.ReadAsStringAsync();
        EmployeeOutput? employee = JsonConvert.DeserializeObject<EmployeeOutput>(responseCreatedContent);
        Assert.NotNull(employee);

        var updatedEmployee = new EmployeeUpdateData(
            Id: employee.Id,
            FirstName: "Maria",
            LastName: "Dolores",
            Dui: 87645521
        );

        HttpClient client = _factory.CreateClient();
        var content = new StringContent(JsonConvert.SerializeObject(updatedEmployee), Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"api/employees", content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var employeeUpdated = JsonConvert.DeserializeObject<EmployeeOutput>(responseContent);

        Assert.NotNull(employeeUpdated);
        Assert.Equal(updatedEmployee.FirstName, employeeUpdated.FirstName);
        Assert.Equal(updatedEmployee.LastName, employeeUpdated.LastName);
        Assert.Equal(updatedEmployee.Dui, employeeUpdated.Dui);
    }

    [Fact]
    public async Task DeleteEmployee_Returns204()
    {
        HttpResponseMessage responseCreated = await createEmployee();

        var responseCreatedContent = await responseCreated.Content.ReadAsStringAsync();
        EmployeeOutput? employee = JsonConvert.DeserializeObject<EmployeeOutput>(responseCreatedContent);
        Assert.NotNull(employee);


        HttpClient client = _factory.CreateClient();
        var response = await client.DeleteAsync($"api/employees/{employee.Id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
