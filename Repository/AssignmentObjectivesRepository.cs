
using FinalProject.Data;
using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repository
{
    public class AssignmentObjectivesRepository : IAssignmentObjectivesRepository
    {
        ApplicationDbContext context;
        public AssignmentObjectivesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void AddAssignmentObjective(AssignmentObjectives assignmentObjective)
        {
            context.assignmentObjectives.Add(assignmentObjective);
            context.SaveChanges();
        }

        public void DeleteAssignmentObjective(int objectiveId, int assignmentId)
        {
            var assignmentObjective = GetAssignmentObjective(objectiveId, assignmentId);
            context.assignmentObjectives.Remove(assignmentObjective);
            context.SaveChanges();
        }

        public void EditAssignmentObjective(AssignmentObjectives assignmentObjective)
        {
            var assignmentObjectives_ = GetAssignmentObjective(assignmentObjective.objectiveId, assignmentObjective.assignmentId);
            assignmentObjectives_.objectiveId = assignmentObjective.objectiveId;
            assignmentObjectives_.assignmentId = assignmentObjective.assignmentId;
            context.SaveChanges();
        }

        public List<AssignmentObjectives> GetAllAssignmentObjectives()
        {
            return context.assignmentObjectives.Include(tc => tc.objective).ToList();
        }

        public AssignmentObjectives GetAssignmentObjective(int objectiveId, int assignmentId)
        {
            var assignmentObjectives_ = context.assignmentObjectives.Where(tc => tc.objectiveId == objectiveId && tc.assignmentId == assignmentId).SingleOrDefault();
            return assignmentObjectives_;
        }

        public void RemoveAssignmentObjectivesByAssignmentId(int assignmentId)
        {
            var objectivesToRemove = context.assignmentObjectives.Where(to => to.assignmentId == assignmentId);
            foreach (var objective in objectivesToRemove)
            {
                context.assignmentObjectives.Remove(objective);
            }
            context.SaveChanges();
        }
    }
}
