
using FinalProject.DTO;
using FinalProject.Entities;
using FinalProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProject.Controllers
{
	public class TrainingController : Controller
    {
        ITrainingRepository trainingRepo;
        IStudentRepository studentRepo;
        ISchoolSupervisorRepository schoolSupervisorRepo;
        ITeamLeaderRepository teamLeaderRepo;
        IObjectiveRepository objectiveRepo;
        ITrainingObjectivesRepository trainingObjectiveRepo;

        public TrainingController(ITrainingRepository trainingRepo, IStudentRepository studentRepo, ISchoolSupervisorRepository schoolSupervisorRepo,
        ITeamLeaderRepository teamLeaderRepo, IObjectiveRepository objectiveRepo, ITrainingObjectivesRepository trainingObjectiveRepo)
        {
            this.trainingRepo = trainingRepo;
            this.studentRepo = studentRepo;
            this.schoolSupervisorRepo = schoolSupervisorRepo;
            this.teamLeaderRepo = teamLeaderRepo;
            this.objectiveRepo = objectiveRepo;
            this.trainingObjectiveRepo = trainingObjectiveRepo;
        }
        [Authorize(Roles = "ADMIN,TEAMLEADER")]
        public IActionResult Index()
        {
            ViewBag.trainings = trainingRepo.GetAllTrainings();
            return View();
        }

        public IActionResult Add() {
            ViewBag.students = studentRepo.GetAllStudents();
            ViewBag.schoolSupervisors = schoolSupervisorRepo.GetAllSchoolSupervisors();
            ViewBag.teamleaders = teamLeaderRepo.GetAllTeamLeaders();
            ViewBag.objectives = objectiveRepo.GetAllObjectives();
			return View();
        }
        

        public IActionResult Insert(TrainingDTO training)
		{
            var training_ = new Training();
            training_.studentId = training.studentId;
            training_.schoolSupervisorId = training.schoolSupervisorId;
            training_.teamLeaderId = training.teamLeaderId;
            training_.startDate = training.startDate;
            training_.endDate = training.endDate;
            trainingRepo.AddTraining(training_ , training.objectiveIds);
            return RedirectToAction("Index");
		}
        

        public IActionResult Edit(int trainingId)
		{
            ViewBag.students = studentRepo.GetAllStudents();
            ViewBag.schoolSupervisors = schoolSupervisorRepo.GetAllSchoolSupervisors();
            ViewBag.teamleaders = teamLeaderRepo.GetAllTeamLeaders();
            ViewBag.trainingObjectivesSelected = trainingObjectiveRepo.GetAllTrainingObjectives().Where(t=> t.trainingId == trainingId).ToList();
            ViewBag.trainingObjectives = objectiveRepo.GetAllObjectives();
            var training = trainingRepo.GetTraining(trainingId);
            var training_ = new TrainingDTO
            {
                trainingId = training.trainingId,
                studentId = training.studentId,
                schoolSupervisorId = training.schoolSupervisorId,
                teamLeaderId = training.teamLeaderId,
                startDate = training.startDate,
                endDate = training.endDate,
            };
            return View(training_);
		}
        

        public IActionResult Edited(TrainingDTO training)
        {
            var training_ = new Training();
            training_.trainingId = training.trainingId;
            training_.studentId = training.studentId;
            training_.schoolSupervisorId = training.schoolSupervisorId;
            training_.teamLeaderId = training.teamLeaderId;
            training_.startDate = training.startDate;
            training_.endDate = training.endDate;
            trainingRepo.EditTraining(training_);
            trainingObjectiveRepo.RemoveTrainingObjectivesByTrainingId(training.trainingId);
            foreach (var o in training.objectiveIds) { 
                 trainingObjectiveRepo.AddTrainingObjective(new TrainingObjectives { trainingId = training.trainingId, objectiveId = o });
            }

            return RedirectToAction("Index");
		}

        public IActionResult Delete(int trainingId)
		{
			trainingRepo.DeleteTraining(trainingId);
			return RedirectToAction("Index");
		}

        //public IActionResult teamLeaderTrainings()
        //{
        //    var teamLeaderId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    if (teamLeaderId == null || teamLeaderRepo.GetTeamLeader(teamLeaderId) == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.trainings = trainingRepo.GetAllTrainings().Where(x => x.teamLeaderId == teamLeaderId).ToList();
        //    return View();
        //}

    }
}
