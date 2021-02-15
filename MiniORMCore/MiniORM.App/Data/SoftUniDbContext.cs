namespace MiniORM.App.Data.Entities
{
    public class SoftUniDbContext : DbContext
    {
        public SoftUniDbContext(string connetionString) : base(connetionString)
        {
        }

        public DbSet<Employees> Employees { get; }

        public DbSet<Projects> Projects { get; }


        public DbSet<Departments> Departments { get; }

        public DbSet<EmployeeProjects> EmployeesProjects { get; }
    }
}