namespace TeisterMask.DataProcessor.ExportDto
{
    using System.Xml.Serialization;

    [XmlType("Project")]
    public class ExportProjectsWithTheirTaskDto
    {
        [XmlAttribute("TasksCount")]
        public string TasksCount { get; set; }

        public string ProjectName { get; set; }

        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        public ExportTaskNameLabelDto[] Tasks { get; set; }
    }
}
