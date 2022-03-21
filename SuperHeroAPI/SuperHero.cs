namespace SuperHeroAPI
{
    public class SuperHero
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
    }
    public class Employee
    {
        public int EmployeeID { get; set; }
        public int EmployeeDeptID { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string Adaccount { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime BornDay { get; set; }
        public DateTime BornTime { get; set; }
        public string CreateUser { get; set; } = string.Empty;
        public DateTime CreateDay { get; set; }
        public string ModifyUser { get; set; } = string.Empty;
        public DateTime UpdateDay { get; set; }

    }


}
