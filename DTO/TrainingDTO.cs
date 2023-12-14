namespace FinalProject.DTO
{
    public class TrainingDTO
    {
        public int trainingId { get; set; }
        public string studentId { get; set; }
        public string teamLeaderId { get; set; }
        public string schoolSupervisorId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public List<int> objectiveIds { get; set; }
    }
}
