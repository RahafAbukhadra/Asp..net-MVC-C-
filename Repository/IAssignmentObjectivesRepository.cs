using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Repository
{
	public interface IAssignmentObjectivesRepository
    {
        public List<AssignmentObjectives> GetAllAssignmentObjectives();
        public void DeleteAssignmentObjective(int objectiveId, int assignmentId);
        public AssignmentObjectives GetAssignmentObjective(int objectiveId, int assignmentId);
        public void EditAssignmentObjective(AssignmentObjectives assignmentObjective);
        public void AddAssignmentObjective(AssignmentObjectives assignmentObjective);
        public void RemoveAssignmentObjectivesByAssignmentId(int assignmentId);
    }
}
