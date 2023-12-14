using FinalProject.DTO;
using FinalProject.Entities;
using FinalProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Apprenticeship.Controllers
{
    public class TeamLeaderController : Controller
    {
        ITeamLeaderRepository teamLeaderRepo;
		ICompanyRepository companyRepo;
		IAssignmentRepository assignmentsRepo;
		IReportRepository reportRepo;
		ITrainingRepository trainingRepo;
		IReportStatusRepository reportStatusRepo;
		IDocumentRepository documentRepo;
		public TeamLeaderController(ITeamLeaderRepository teamLeaderRepo, IDocumentRepository documentRepo, ICompanyRepository companyRepo,IReportStatusRepository reportStatusRepo ,IAssignmentRepository assignmentsRepo, IReportRepository reportRepo, ITrainingRepository trainingRepo)
		{
			this.teamLeaderRepo = teamLeaderRepo;
			this.companyRepo = companyRepo;
			this.assignmentsRepo = assignmentsRepo;
			this.trainingRepo = trainingRepo;
			this.documentRepo = documentRepo;
			this.reportRepo = reportRepo;
			this.reportStatusRepo = reportStatusRepo;
		}
        [Authorize(Roles = "ADMIN")]
        public IActionResult Index()
        {
            ViewBag.teamLeaders = teamLeaderRepo.GetAllTeamLeaders();
            return View();
        }
  
        public IActionResult Add() {
            ViewBag.companies = companyRepo.GetAllCompanies();
            return View();
        }
       
        public async Task<IActionResult> Insert(TeamLeaderDTO teamLeader)
		{
			var teamLeader_ = new TeamLeader();
            teamLeader_.fristName = teamLeader.fristName;
            teamLeader_.secondName = teamLeader.secondName;
            teamLeader_.thirdName = teamLeader.thirdName;
            teamLeader_.lastName = teamLeader.lastName;
            teamLeader_.PhoneNumber = teamLeader.PhoneNumber;
            teamLeader_.Email = teamLeader.Email;
            teamLeader_.NormalizedEmail = teamLeader.Email.ToUpper();
            teamLeader_.UserName = teamLeader.Email;
            teamLeader_.NormalizedUserName = teamLeader.Email.ToUpper();
            teamLeader_.companyId = teamLeader.companyId;
            await teamLeaderRepo.AddTeamLeader(teamLeader_, teamLeader.Password);
			return RedirectToAction("Index");
		}
     

        public IActionResult Edit(string Id)
		{
            ViewBag.companies = companyRepo.GetAllCompanies();
            var teamLeader_ = teamLeaderRepo.GetTeamLeader(Id);
			return View(teamLeader_);
		}
        

        public IActionResult Edited(TeamLeaderDTO teamLeader)
		{
			var teamLeader_ = new TeamLeader();
			teamLeader_.Id = teamLeader.Id;
			teamLeader_.fristName = teamLeader.fristName;
			teamLeader_.secondName = teamLeader.secondName;
			teamLeader_.thirdName = teamLeader.thirdName;
			teamLeader_.lastName = teamLeader.lastName;
			teamLeader_.PhoneNumber = teamLeader.PhoneNumber;
			teamLeader_.companyId = teamLeader.companyId;
			teamLeaderRepo.EditTeamLeader(teamLeader_);
			return RedirectToAction("Index");
		}

        public IActionResult Delete(string Id)
		{
			teamLeaderRepo.DeleteTeamLeader(Id);
			return RedirectToAction("Index");
		}

        public IActionResult Trainings()
		{
			var teamLeaderId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			ViewBag.trainings=trainingRepo.GetAllTrainings().Where(t => t.teamLeaderId == teamLeaderId).ToList();
			var teamLeader = teamLeaderRepo.GetTeamLeader(teamLeaderId);
			return View(teamLeader);
		}
        public IActionResult Assignments(int trainingId)
		{
			ViewBag.assignments = assignmentsRepo.GetAllAssignments().Where(t => t.trainingId == trainingId).ToList();
			ViewBag.documents = documentRepo.GetAllDocuments();

			return View(trainingId);
		}
        public IActionResult Reports(int assignmentId)
		{
			ViewBag.reports = reportRepo.GetAllReports().Where(r => r.assignmentId == assignmentId).ToList();
			ViewBag.reportStatuses = reportStatusRepo.GetAllReportStatuses();
            ViewBag.documents = documentRepo.GetAllDocuments();
            return View(assignmentId);
		}
	}
}
