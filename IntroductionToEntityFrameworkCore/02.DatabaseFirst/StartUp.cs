using SoftUni.Data;
using System.Linq;
using System;
using System.Text;
using SoftUni.Models;
using System.Globalization;
using System.Collections.Generic;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (SoftUniContext context = new SoftUniContext())
            {
                string result = RemoveTown(context);
                Console.WriteLine(result);
            }
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary,
                    e.EmployeeId
                })
                .OrderBy(x => x.EmployeeId);
            StringBuilder sb = new StringBuilder();
            
            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary,
                })
                .OrderBy(x => x.FirstName)
                .Where(e => e.Salary > 50000);
            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                    sb.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Salary,
                    e.Department,
                    e.Department.Name,
                })
                .OrderBy(x => x.Salary)
                .ThenByDescending(x => x.FirstName);

            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from Research and Development - ${employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var address = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            context.Addresses.Add(address);

            var nakov = context.Employees.FirstOrDefault(x => x.LastName == "Nakov");
            nakov.Address = address;

            context.SaveChanges();

            var employeeAddresses = context.Employees
                .OrderByDescending(x => x.AddressId)
                .Select(x => x.Address.AddressText)
                .Take(10)
                .ToList();

            foreach (var ea in employeeAddresses)
            {
                sb.AppendLine($"{ea}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(x => x.EmployeesProjects
                    .Any(s => s.Project.StartDate.Year >= 2001 && s.Project.StartDate.Year <= 2003))
                .Select(x => new
                {
                    EmployeeFullName = x.FirstName + " " + x.LastName,
                    ManagerFullName = x.Manager.FirstName + " " + x.Manager.LastName,
                    Projects = x.EmployeesProjects.Select(s => new
                    {
                        ProjectName = s.Project.Name,
                        StartDate = s.Project.StartDate,
                        EndDate = s.Project.EndDate,
                    }).ToList()
                })
                .Take(10)
                .ToList();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.EmployeeFullName} - Manager: {e.ManagerFullName}");

                foreach (var project in e.Projects)
                {
                    string startDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    string endDate = project.EndDate.HasValue ?
                        project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) : "not finished";

                    sb.AppendLine($"--{project.ProjectName} - {startDate} - {endDate}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var selectedAddresses = context.Addresses
                   .OrderByDescending(a => a.Employees.Count)
                   .ThenBy(a => a.Town.Name)
                   .ThenBy(a => a.AddressText)
                   .Take(10)
                   .Select(a => new
                   {
                       Text = a.AddressText,
                       Town = a.Town.Name,
                       EmployeesCount = a.Employees.Count
                   })
                   .ToList();

            foreach (var address in selectedAddresses)
            {
                sb.AppendLine($"{address.Text}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            int SearchedEmployeeId = 147;

            var searchedEmployee = context.Employees
                     .Select(e => new
                     {
                         e.EmployeeId,
                         e.FirstName,
                         e.LastName,
                         e.JobTitle,
                         Projects = e.EmployeesProjects.Select(ep => new
                         {
                             ep.Project.Name
                         })
                     })
                     .FirstOrDefault(e => e.EmployeeId == SearchedEmployeeId);

            if (searchedEmployee != null)
            {
                sb.AppendLine($"{searchedEmployee.FirstName} {searchedEmployee.LastName} - {searchedEmployee.JobTitle}");

                foreach (var project in searchedEmployee.Projects.OrderBy(ep => ep.Name))
                {
                    sb.AppendLine($"{project.Name}");
                }
            }
            else
            {
                sb.AppendLine($"Employee with ID {SearchedEmployeeId} was not found.");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var departments = context.Departments
                .Where(e => e.Employees.Count > 5)
                .OrderBy(e => e.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(x => new
                {
                    DepartmentName = x.Name,
                    ManagerFullName = x.Manager.FirstName + " " + x.Manager.LastName,
                    Employees = x.Employees.Select(e => new
                    {
                        EmployeeFullName = e.FirstName + " " + e.LastName,
                        JobTitle = e.JobTitle
                    })
                    .OrderBy(f => f.EmployeeFullName)
                    .ToList()
                }).ToList();

            foreach (var department in departments)
            {
                sb.AppendLine($"{department.DepartmentName} - {department.ManagerFullName}");

                foreach (var employee in department.Employees)
                {
                    sb.AppendLine($"{employee.EmployeeFullName} - {employee.JobTitle}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var latestTenProjects = context.Projects
                    .OrderByDescending(p => p.StartDate)
                    .Take(10)
                    .OrderBy(p => p.Name)
                    .Select(p => new
                    {
                        p.Name,
                        p.Description,
                        p.StartDate
                    })
                    .ToList();

            foreach (var project in latestTenProjects)
            {
                sb.AppendLine(project.Name);
                sb.AppendLine(project.Description);
                sb.AppendLine(project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
            }

            return sb.ToString().TrimEnd();
        }

        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            string[] targetedDepartments = { "Engineering", "Tool Design", "Marketing", "Information Services" };

            List<Employee> targetedEmployees = context.Employees
                .Where(e => targetedDepartments.Contains(e.Department.Name))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            foreach (Employee emp in targetedEmployees)
            {
                emp.Salary *= 1.12m;
                sb.AppendLine($"{emp.FirstName} {emp.LastName} (${emp.Salary:F2})");
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var searchedEmployees = context.Employees
                   .Where(e => e.FirstName.StartsWith("Sa"))
                   .OrderBy(e => e.FirstName)
                   .ThenBy(e => e.LastName)
                   .Select(e => new
                   {
                       e.FirstName,
                       e.LastName,
                       e.JobTitle,
                       e.Salary
                   })
                   .ToList();

            foreach (var emp in searchedEmployees)
            {
                sb.AppendLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle} - (${emp.Salary:F2})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string DeleteProjectById(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            const int ProjectToBeDeletedId = 2;

            Project projectToBeDeleted = context.Projects.Find(ProjectToBeDeletedId);
            List<EmployeeProject> employeeProjectsToBeDeleted = context.EmployeesProjects.Where(ep => ep.Project.ProjectId == ProjectToBeDeletedId).ToList();

            context.EmployeesProjects.RemoveRange(employeeProjectsToBeDeleted);
            context.SaveChanges();

            context.Remove(projectToBeDeleted);
            context.SaveChanges();

            foreach (string projectName in context.Projects.Select(p => p.Name).Take(10))
            {
                sb.AppendLine(projectName);
            }

            return sb.ToString().TrimEnd();
        }

        public static string RemoveTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            string townToBeDeletedName = "Seattle";
            List<Address> addressesFromTheTargetTown = context.Addresses
                .Where(a => a.Town.Name.Equals(townToBeDeletedName))
                .ToList();

            foreach (Employee employee in context.Employees)
            {
                if (addressesFromTheTargetTown.Contains(employee.Address))
                {
                    employee.Address = null;
                }
            }

            context.Addresses.RemoveRange(addressesFromTheTargetTown);
            Town townToBeDeleted = context.Towns.SingleOrDefault(t => t.Name.Equals(townToBeDeletedName));
            context.Towns.Remove(townToBeDeleted);
            context.SaveChanges();

            int removedAddressesCount = addressesFromTheTargetTown.Count;
            string addressSingleOrPlural;
            string wasWere;

            if (removedAddressesCount > 1)
            {
                addressSingleOrPlural = "addresses";
                wasWere = "were";
            }
            else
            {
                addressSingleOrPlural = "address";
                wasWere = "was";
            }

            sb.AppendLine($"{removedAddressesCount} {addressSingleOrPlural} in {townToBeDeletedName} {wasWere} deleted");

            return sb.ToString().TrimEnd();
        }

    }
}
