namespace Employees.Management.Models
{
    public class User
    {
        private User() { }

        public User(string userName, string password, string? id = null)
        {
            Id = id ?? Guid.NewGuid().ToString();
            UserName = userName;
            PassWord = password;
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }

    }
}
