using System.Reflection.Metadata;

namespace FinalProject.DTO
{
    public class AssignmentDTO
    {
        public int assignmentId { get; set; }
        public string assignmentTitle { get; set; }
        public string assignmentDescription { get; set; }
        public string assignmentNotes { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int trainingId { get; set; }
        public List<int> objectiveIds { get; set; }
    }
}
