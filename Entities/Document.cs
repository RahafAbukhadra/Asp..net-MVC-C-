namespace FinalProject.Entities
{
	public class Document
	{
        public int documentId { get; set; }
        public string name { get; set; }
		public string contentType { get; set; }
		public byte[] file { get; set; }
        public Report report { get; set; }
        public int? reportId { get; set; }
		public Assignment assignment { get; set; }
		public int? assignmentId { get; set; }
		public ReportsLog reportsLog{ get; set; }
		public int? reportsLogId { get; set; }
	}
}
