
using FinalProject.DTO;
using FinalProject.Entities;
using FinalProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProject.Controllers
{
	public class ObjectiveController : Controller
    {
        IObjectiveRepository objectiveRepo;
        public ObjectiveController(IObjectiveRepository objectiveRepo)
        {
            this.objectiveRepo = objectiveRepo;
        }
        //Read From Database
        [Authorize(Roles = " TEAMLEADER, STUDENT, SCHOOLSUPERVISOR")]

        public IActionResult Index()
        {
            ViewBag.objectives = objectiveRepo.GetAllObjectives();
            return View();
        }

        public IActionResult Add() {
            return View();
        } 

        public IActionResult Insert(ObjectiveDTO objective)
		{
            var objective_ = new Objective();
            objective_.objectiveName = objective.objectiveName;
            objectiveRepo.AddObjective(objective_);
            return RedirectToAction("Index");
		}
        public IActionResult Edit(int objectiveId)
		{
            var objective_ = objectiveRepo.GetObjective(objectiveId);
			return View(objective_);
		}
       
        public IActionResult Edited(ObjectiveDTO objective)
		{
			var objective_ = new Objective();
			objective_.objectiveId = objective.objectiveId;
			objective_.objectiveName = objective.objectiveName;


			objectiveRepo.EditObjective(objective_);
			return RedirectToAction("Index");
		}
       
       
        public IActionResult Delete(int objectiveId)
		{
			objectiveRepo.DeleteObjective(objectiveId);
			return RedirectToAction("Index");
		}
	}
}
