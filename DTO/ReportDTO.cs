﻿namespace FinalProject.DTO
{
    public class ReportDTO
    {
        public int reportId { get; set; }
        public string reportName { get; set; }
        public string reportDescription { get; set; }
        public string reportNotes { get; set; }
        public int assignmentId { get; set; }
        public int reportStatusId { get; set; }
    }
}
