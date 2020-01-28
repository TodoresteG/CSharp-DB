namespace TeisterMask.DataProcessor.ExportDto
{
    public class ExportMostBusyEmployeeDto
    {
        public string Username { get; set; }

        public ExportTaskDto[] Tasks { get; set; }
    }
}
