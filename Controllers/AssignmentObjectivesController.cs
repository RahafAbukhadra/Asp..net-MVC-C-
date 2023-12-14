
using FinalProject.DTO;
using FinalProject.Entities;
using FinalProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProject.Controllers
{
	public class AssignmentObjectivesController : Controller
    {
        IAssignmentObjectivesRepository assignmentObjectivesRepo;
        IAssignmentRepository assignmentRepo;
        IObjectiveRepository objectivesRepo;
        public AssignmentObjectivesController(IAssignmentObjectivesRepository assignmentObjectivesRepo, IAssignmentRepository assignmentRepo, IObjectiveRepository objectivesRepo)
        {
            this.assignmentObjectivesRepo = assignmentObjectivesRepo;
            this.assignmentRepo = assignmentRepo;
            this.objectivesRepo = objectivesRepo;
        }
        //Read From Database
        [Authorize(Roles = "TEAMLEADER, STUDENT")]
        public IActionResult Index()
        {
            ViewBag.assignmentObjectives = assignmentObjectivesRepo.GetAllAssignmentObjectives();
            return View();
        }
        public IActionResult Add()
        {
            ViewBag.trainings = assignmentRepo.GetAllAssignments();
            ViewBag.objectives = objectivesRepo.GetAllObjectives();

            return View();
        }
        //Create assignmentObjectives Row in Database
        public IActionResult Insert(AssignmentObjectiveDTO assignmentObjective)
        {
            var assignmentObjective_ = new AssignmentObjectives();
            assignmentObjective_.objectiveId = assignmentObjective.objectiveId;
            assignmentObjective_.assignmentId = assignmentObjective.assignmentId;
            assignmentObjectivesRepo.AddAssignmentObjective(assignmentObjective_);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(AssignmentObjectiveDTO assignmentObjective)
        {
            var assignmentObjective_ = assignmentObjectivesRepo.GetAssignmentObjective(assignmentObjective.objectiveId, assignmentObjective.assignmentId);
            return View(assignmentObjective_);
        }
        //Edit assignmentObjectives Row in Database
        public IActionResult Edited(ObjectiveDTO objective)
        {
            var assignmentObjective_ = new AssignmentObjectives();
            assignmentObjectivesRepo.EditAssignmentObjective(assignmentObjective_);
            return RedirectToAction("Index");
        }
        //Delete assignmentObjectives Row in Database

     
        public IActionResult Delete(AssignmentObjectiveDTO assignmentObjective)
        {
            assignmentObjectivesRepo.DeleteAssignmentObjective(assignmentObjective.objectiveId, assignmentObjective.assignmentId);
            return RedirectToAction("Index");
        }
    }
}
