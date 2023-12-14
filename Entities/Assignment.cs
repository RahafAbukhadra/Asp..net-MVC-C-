namespace FinalProject.Entities
{
    public class Assignment
    {
        public int assignmentId { get; set; }
        public string assignmentTitle { get; set; }
        public string assignmentDescription { get; set; }
        public string assignmentNotes { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Training training { get; set; }
        public int trainingId { get; set; }
        public List<Report> reports { get; set; }
        public List<AssignmentObjectives> assignmentObjectives { get; set; }
		public Document document { get; set; }
	}
}
