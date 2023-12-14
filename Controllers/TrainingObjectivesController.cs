
using FinalProject.DTO;
using FinalProject.Entities;
using FinalProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProject.Controllers
{
	[Authorize(Roles = "ADMIN,TEAMLEADER,STUDENT, SCHOOLSUPERVISOR")]
    public class TrainingObjectivesController : Controller
	{
		ITrainingObjectivesRepository trainingObjectivesRepo;
		ITrainingRepository trainingRepo;
		IObjectiveRepository objectivesRepo;
        public TrainingObjectivesController(ITrainingObjectivesRepository trainingObjectivesRepo, ITrainingRepository trainingRepo, IObjectiveRepository objectivesRepo)
		{
			this.trainingObjectivesRepo = trainingObjectivesRepo;
			this.trainingRepo = trainingRepo;
			this.objectivesRepo = objectivesRepo;
		}
		//Read From Database
		public IActionResult Index()
		{
			ViewBag.trainingObjectives = trainingObjectivesRepo.GetAllTrainingObjectives();
			return View();
		}

		public IActionResult Add()
		{
            ViewBag.trainings = trainingRepo.GetAllTrainings();
            ViewBag.objectives = objectivesRepo.GetAllObjectives();

            return View();
		}
        //Create TrainingObjectives Row in Database
        public IActionResult Insert(TrainingObjectiveDTO trainingObjective)
		{
			var trainingObjective_ = new TrainingObjectives();
			trainingObjective_.objectiveId = trainingObjective.objectiveId;
			trainingObjective_.trainingId = trainingObjective.trainingId;
			trainingObjectivesRepo.AddTrainingObjective(trainingObjective_);
			return RedirectToAction("Index");
		}

		public IActionResult Edit(TrainingObjectiveDTO trainingObjective)
		{
			var trainingObjective_ = trainingObjectivesRepo.GetTrainingObjective(trainingObjective.objectiveId, trainingObjective.trainingId);
			return View(trainingObjective_);
		}
        //Edit TrainingObjectives Row in Database
        public IActionResult Edited(ObjectiveDTO objective)
		{
			var trainingObjective_ = new TrainingObjectives();
			trainingObjectivesRepo.EditTrainingObjective(trainingObjective_);
			return RedirectToAction("Index");
		}
        //Delete TrainingObjectives Row in Database
        public IActionResult Delete(TrainingObjectiveDTO trainingObjective)
		{
			trainingObjectivesRepo.DeleteTrainingObjective(trainingObjective.objectiveId, trainingObjective.trainingId);
			return RedirectToAction("Index");
		}
	}
}
