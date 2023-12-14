using FinalProject.Entities;

namespace FinalProject.DTO
{
    public class DocumentDTO
    {
        public int documentId { get; set; }
        public string name { get; set; }
        public string contentType { get; set; }
        public byte[] file { get; set; }
        public int reportId { get; set; }
        public int assignmentId { get; set; }
        public int reportsLogId { get; set; }
    }
}
