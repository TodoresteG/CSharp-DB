namespace TeisterMask.DataProcessor
{
    using Data;
    using ExportDto;

    using System;
    using System.Linq;
    using System.Globalization;
    using Formatting = Newtonsoft.Json.Formatting;
    using Newtonsoft.Json;
    using System.Xml.Serialization;
    using System.Xml;
    using System.IO;
    using System.Text;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context
                .Projects
                .Where(p => p.Tasks.Any())
                .OrderByDescending(p => p.Tasks.Count)
                .ThenBy(p => p.Name)
                .Select(p => new ExportProjectsWithTheirTaskDto
                {
                    TasksCount = p.Tasks.Count.ToString(),
                    ProjectName = p.Name,
                    HasEndDate = p.DueDate != null ? "Yes" : "No",
                    Tasks = p.Tasks.Select(t => new ExportTaskNameLabelDto 
                    {
                        Name = t.Name,
                        Label = t.LabelType.ToString()
                    })
                    .OrderBy(t => t.Name)
                    .ToArray()
                })
                .ToArray();

            var serializer = new XmlSerializer(typeof(ExportProjectsWithTheirTaskDto[]), new XmlRootAttribute("Projects"));

            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8
            };

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            var stringWriter = new StringWriter();

            using (var writer = XmlWriter.Create(stringWriter, settings))
            {
                serializer.Serialize(stringWriter, projects, namespaces);
            }

            return stringWriter.ToString();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context
                .Employees
                .Where(e => e.EmployeesTasks.Any(et => et.Task.OpenDate >= date))
                .Select(e => new ExportMostBusyEmployeeDto
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks
                    .Where(et => et.Task.OpenDate >= date)
                    .OrderByDescending(et => et.Task.DueDate)
                    .ThenBy(t => t.Task.Name)
                    .Select(et => new ExportTaskDto
                    {
                        TaskName = et.Task.Name,
                        OpenDate = et.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                        DueDate = et.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        LabelType = et.Task.LabelType.ToString(),
                        ExecutionType = et.Task.ExecutionType.ToString()
                    })
                    .ToArray()
                })
                .OrderByDescending(GetCount)
                .ThenBy(e => e.Username)
                .Take(10)
                .ToArray();

            var employeesJson = JsonConvert.SerializeObject(employees, Formatting.Indented);

            return employeesJson;
        }

        private static int GetCount(ExportMostBusyEmployeeDto dto)
        {
            return dto.Tasks.Count();
        }
    }
}