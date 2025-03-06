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

    [Fact]
    public async Task CreateEmployee_Returns200()
    {
        var newEmployee = new EmployeeCreationData(
            Id: Guid.NewGuid().ToString(),
            FirstName: "Maria",
            LastName: "Dolores",
            Dui: 87645521
        );

        HttpClient client = _factory.CreateClient();
        var employe = new StringContent(JsonConvert.SerializeObject(newEmployee), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("api/employees", employe);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var createdEmployee = JsonConvert.DeserializeObject<EmployeeOutput>(responseContent);

        Assert.NotNull(createdEmployee);
        Assert.Equal(newEmployee.FirstName, createdEmployee.FirstName);
        Assert.Equal(newEmployee.LastName, createdEmployee.LastName);
        Assert.Equal(newEmployee.Dui, createdEmployee.Dui);
        Assert.NotNull(createdEmployee.Id);
    }

    [Fact]
    public async Task GetEmployee_Returns200()
    {
        HttpClient client = _factory.CreateClient();
        var response = await client.GetAsync("api/employees/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var employee = JsonConvert.DeserializeObject<EmployeeOutput>(responseContent);

        Assert.NotNull(employee);
        Assert.Equal("1", employee.Id);
    }

    [Fact]
    public async Task UpdateEmployee_Returns200()
    {
        var updatedEmployee = new EmployeeUpdateData(
            Id: "5461213132",
            FirstName: "Maria",
            LastName: "Dolores",
            Dui: 87645521
        );

        HttpClient client = _factory.CreateClient();
        var content = new StringContent(JsonConvert.SerializeObject(updatedEmployee), Encoding.UTF8, "application/json");

        var response = await client.PutAsync("api/employees/0d96732b-e75e-4542-95ac-7dafabaa6a6f", content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var employee = JsonConvert.DeserializeObject<EmployeeOutput>(responseContent);

        Assert.NotNull(employee);
        Assert.Equal(updatedEmployee.FirstName, employee.FirstName);
        Assert.Equal(updatedEmployee.LastName, employee.LastName);
        Assert.Equal(updatedEmployee.Dui, employee.Dui);
    }

    [Fact]
    public async Task DeleteEmployee_Returns200()
    {
        HttpClient client = _factory.CreateClient();
        var response = await client.DeleteAsync("api/employees/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var deletedEmployee = JsonConvert.DeserializeObject<EmployeeOutput>(responseContent);

        Assert.NotNull(deletedEmployee);
        Assert.Equal("1", deletedEmployee.Id);
    }
}

