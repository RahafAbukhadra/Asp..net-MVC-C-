using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Repository
{
	public interface IAssignmentRepository
    {
        public List<Assignment> GetAllAssignments();
        public void DeleteAssignment(int assignmentId, int trainingId);
        public Assignment GetAssignment(int assignmentId, int trainingId);
        public void EditAssignment(Assignment assignment);
        public void AddAssignment(Assignment assignment, List<int> objectiveIds);
    }
}
