using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Repository
{
	public interface ITrainingRepository
    {
        public List<Training> GetAllTrainings();
        public void DeleteTraining(int trainingId);
        public Training GetTraining(int trainingId);
        public void EditTraining(Training training);
        public void AddTraining(Training training,List<int> objectiveIds);
    }
}
