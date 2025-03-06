namespace Employees.Management.Models
{
    public class Employee
    {
        private Employee() { }

        public Employee(string firstName, string lastName, int dui, string? id = null)
        {
            Id = id ?? Guid.NewGuid().ToString();
            FirstName = firstName;
            LastName = lastName;
            Dui = dui;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Dui { get; set; }

    }
}
