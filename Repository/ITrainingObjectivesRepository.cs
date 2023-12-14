using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Repository
{
	public interface ITrainingObjectivesRepository
    {
            public List<TrainingObjectives> GetAllTrainingObjectives();
            public void DeleteTrainingObjective(int objectiveId, int trainingId);
            public TrainingObjectives GetTrainingObjective(int objectiveId, int trainingId);
            public void EditTrainingObjective(TrainingObjectives trainingObjective);
            public void AddTrainingObjective(TrainingObjectives trainingObjective);
            public void RemoveTrainingObjectivesByTrainingId(int trainingId);
        }
}