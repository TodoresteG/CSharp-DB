namespace SoftUni
{
    using Data;
    using SoftUni.Models;
    using System;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                string result = GetEmployeesByFirstNameStartingWithSa(context);

                Console.WriteLine(result);
            }
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var emp in context.Employees)
            {
                sb.AppendLine($"{emp.FirstName} {emp.LastName} {emp.MiddleName} {emp.JobTitle} {emp.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName);

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FirstName} - {emp.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Salary
                })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName);

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FirstName} {emp.LastName} from Research and Development - ${emp.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            Address address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            Employee employee = context.Employees
                .FirstOrDefault(e => e.LastName == "Nakov");

            employee.Address = address;

            context.SaveChanges();

            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Select(e => new
                {
                    e.AddressId,
                    e.Address.AddressText
                })
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .ToArray();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.AddressText}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.EmployeesProjects.Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                .Take(10)
                .Select(e => new
                {
                    EmployeeName = e.FirstName + " " + e.LastName,
                    ManagaerName = e.Manager.FirstName + " " + e.Manager.LastName,
                    Projects = e.EmployeesProjects.Select(p => new
                    {
                        ProjectName = p.Project.Name,
                        p.Project.StartDate,
                        p.Project.EndDate
                    })
                    .ToList()
                })
                .ToList();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.EmployeeName} - Manager: {emp.ManagaerName}");

                foreach (var project in emp.Projects)
                {
                    string startDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt");

                    string endDate = project.EndDate.HasValue ? project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt") : "not finished";

                    sb.AppendLine($"--{project.ProjectName} - {startDate} - {endDate}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var addresses = context.Addresses
                .Select(a => new
                {
                    a.AddressText,
                    a.Town.Name,
                    a.Employees.Count
                })
                .OrderByDescending(a => a.Count)
                .ThenBy(a => a.Name)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .ToList();

            foreach (var a in addresses)
            {
                sb.AppendLine($"{a.AddressText}, {a.Name} - {a.Count} employees");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employee = context.Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    e.JobTitle,
                    e.FirstName,
                    e.LastName,
                    Projects = e.EmployeesProjects
                    .Select(ep => new
                    {
                        ep.Project.Name
                    })
                    .ToList()
                })
                .FirstOrDefault(e => e.EmployeeId == 147);

            sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

            foreach (var p in employee.Projects.OrderBy(p => p.Name))
            {
                sb.AppendLine($"{p.Name}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var departments = context.Departments
                .Select(d => new
                {
                    d.Employees.Count,
                    d.Name,
                    ManagerName = d.Manager.FirstName + " " + d.Manager.LastName,
                    Employees = d.Employees.Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle
                    })
                })
                .Where(d => d.Count > 5)
                .OrderBy(d => d.Count)
                .ThenBy(d => d.Name)
                .ToList();

            foreach (var d in departments)
            {
                sb.AppendLine($"{d.Name} - {d.ManagerName}");

                foreach (var e in d.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                {
                    sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name)
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    p.StartDate
                });

            foreach (var p in projects)
            {
                sb.AppendLine($"{p.Name}");
                sb.AppendLine($"{p.Description}");
                sb.AppendLine($"{p.StartDate.ToString("M/d/yyyy h:mm:ss tt")}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.Department.Name == "Engineering" || e.Department.Name == "Tool Design"
                        || e.Department.Name == "Marketing" || e.Department.Name == "Information Services")
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            foreach (var e in employees)
            {
                e.Salary += e.Salary * 0.12m;
            }

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();


            var employees = context.Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string DeleteProjectById(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            int projectId = 2;

            var employeesProjects = context.EmployeesProjects
                .Where(ep => ep.ProjectId == projectId);

            foreach (var employeeProject in employeesProjects)
            {
                context.EmployeesProjects.Remove(employeeProject);
            }

            var project = context.Projects.Find(projectId);
            context.Projects.Remove(project);
            context.SaveChanges();

            var projectsNames = context.Projects
                .Take(10)
                .Select(p => p.Name);

            foreach (var projectName in projectsNames)
            {
                stringBuilder.AppendLine(projectName);
            }

            return stringBuilder.ToString().TrimEnd();
        }

        public static string RemoveTown(SoftUniContext context)
        {
            var addresses = context.Addresses
                .Where(a => a.Town.Name == "Seattle")
                .ToList();

            context.Employees
                .Where(e => addresses
                    .Any(a => a.AddressId == e.AddressId))
                .ToList()
                .ForEach(e => e.AddressId = null);

            context.Addresses.RemoveRange(addresses);

            var town = context.Towns
                .FirstOrDefault(t => t.Name == "Seattle");

            context.Towns.Remove(town);
            context.SaveChanges();

            var result = $"{addresses.Count} addresses in Seattle were deleted";

            return result;
        }
    }
}
