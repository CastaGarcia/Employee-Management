using Management.Inputs;
using Management.Outputs;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace User.Management.Api.IntegrationTest
{
    public class UserControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UserControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }   

        async Task<HttpResponseMessage> createUser()
        {
            UserCreationData newUser = new UserCreationData(
                Id: Guid.NewGuid().ToString(),
                UserName: "Maria",
                PassWord: "Dolores");

            HttpClient client = _factory.CreateClient();
            var user = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

            return await client.PostAsync("api/users", user);
        }

        [Fact]
        public async Task CreateUser_Returns200()
        {
            HttpResponseMessage response = await createUser();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            UserOutput? user = JsonConvert.DeserializeObject<UserOutput>(responseContent);

            Assert.NotNull(user);
            Assert.NotNull(user.Id);
        }

        [Fact]
        public async Task GetUser_Returns200()
        {
            HttpResponseMessage responseCreated = await createUser();

            var responseCreatedContent = await responseCreated.Content.ReadAsStringAsync();
            UserOutput? user = JsonConvert.DeserializeObject<UserOutput>(responseCreatedContent);

            HttpClient client = _factory.CreateClient();
            var response = await client.GetAsync($"api/users/{user?.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var employee2 = JsonConvert.DeserializeObject<UserOutput>(responseContent);

            Assert.NotNull(user);
            Assert.Equal(user.Id, user.Id);
        }

        [Fact]
        public async Task UpdateUser_Returnssuccess200()
        {
            HttpResponseMessage responseSucces = await createUser();

            var responseCreatedContent = await responseSucces.Content.ReadAsStringAsync();
            UserOutput? user = JsonConvert.DeserializeObject<UserOutput>(responseCreatedContent);
            Assert.NotNull(user);

           var updateUser = new UserUpdateData(
                Id: user.Id,
                UserName: "Maria",
                PassWord: "Dolores");

            HttpClient client = _factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(updateUser), Encoding.UTF8, "application/json");

            var response = await client.PutAsync("api/users", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var userUpdated = JsonConvert.DeserializeObject<UserOutput>(responseContent);

            Assert.NotNull(userUpdated);
            Assert.Equal(userUpdated.Id, user.Id);
            Assert.Equal(userUpdated.UserName, updateUser.UserName);
            Assert.Equal(userUpdated.Password, updateUser.PassWord);
        }

        [Fact]
        public async Task DeleteUser_Returns204()
        {
            HttpResponseMessage responseCreated = await createUser();

            var responseCreatedContent = await responseCreated.Content.ReadAsStringAsync();
            UserOutput? user = JsonConvert.DeserializeObject<UserOutput>(responseCreatedContent);
            Assert.NotNull(user);

            HttpClient client = _factory.CreateClient();
            var response = await client.DeleteAsync($"api/users/{user.Id}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

    }
}
