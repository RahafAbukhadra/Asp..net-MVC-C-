using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Repository
{
	public interface IObjectiveRepository
    {
        public List<Objective> GetAllObjectives();
        public void DeleteObjective(int objectiveId);
        public Objective GetObjective(int objectiveId);
        public void EditObjective(Objective objective);
        public void AddObjective(Objective objective);
    }
}
