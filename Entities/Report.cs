namespace FinalProject.Entities
{
    public class Report
    {
        public int reportId { get; set; }
        public string reportName { get; set; }
        public string reportDescription { get; set; }
        public string reportNotes { get; set; }
        public Assignment assignment { get; set; }
        public int assignmentId { get; set; }
        public ReportStatus reportStatus { get; set; }
        public int reportStatusId { get; set; }
        public Document document { get; set; }
    }
}
