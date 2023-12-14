namespace FinalProject.Entities
{
    public class AssignmentObjectives
    {
        public Assignment assignment { get; set; }
        public int assignmentId { get; set; }
        public Objective objective { get; set; }
        public int objectiveId { get; set; }
    }
}
