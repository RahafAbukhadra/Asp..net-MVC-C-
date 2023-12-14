
using FinalProject.DTO;
using FinalProject.Entities;
using FinalProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProject.Controllers
{
	public class SchoolSupervisorController : Controller
    {
        ISchoolSupervisorRepository schoolSupervisorRepo;
        ISchoolRepository schoolRepo;
        ITrainingRepository trainingRepo;
        IAssignmentRepository assignmentRepo;
        IReportRepository reportRepo;
        IReportsLogRepository reportsLogRepo;
        IDocumentRepository documentRepo;
        IReportStatusRepository reportStatusRepo;
        public SchoolSupervisorController(ISchoolSupervisorRepository schoolSupervisorRepo, 
            ISchoolRepository schoolRepo, ITrainingRepository trainingRepo, IAssignmentRepository assignmentRepo, IReportStatusRepository reportStatusRepo, IDocumentRepository documentRepo,

			IReportRepository reportRepo,IReportsLogRepository reportsLogRepo){
            this.schoolSupervisorRepo = schoolSupervisorRepo;
            this.schoolRepo = schoolRepo;
            this.trainingRepo = trainingRepo;
            this.assignmentRepo = assignmentRepo;
            this.reportRepo = reportRepo;
            this.reportsLogRepo = reportsLogRepo;
            this.documentRepo = documentRepo;
            this.reportStatusRepo = reportStatusRepo;
        }
        //Read From Database
        [Authorize(Roles = "ADMIN")]
        public IActionResult Index()
        {
            ViewBag.schoolSupervisors = schoolSupervisorRepo.GetAllSchoolSupervisors();
            return View();
        }
        public IActionResult Add()
        {
            ViewBag.schools = schoolRepo.GetAllSchools();
            return View();
        }
    
        public async Task<IActionResult> Insert(SchoolSupervisorDTO schoolSupervisor)
        {
            var schoolSupervisor_ = new SchoolSupervisor();
            schoolSupervisor_.fristName = schoolSupervisor.fristName;
            schoolSupervisor_.secondName = schoolSupervisor.secondName;
            schoolSupervisor_.thirdName = schoolSupervisor.thirdName;
            schoolSupervisor_.lastName = schoolSupervisor.lastName;
            schoolSupervisor_.PhoneNumber = schoolSupervisor.PhoneNumber;
            schoolSupervisor_.Email = schoolSupervisor.Email;
            schoolSupervisor_.NormalizedEmail = schoolSupervisor.Email.ToUpper();
            schoolSupervisor_.UserName = schoolSupervisor.Email;
            schoolSupervisor_.NormalizedUserName = schoolSupervisor.Email.ToUpper();
            schoolSupervisor_.schoolId = schoolSupervisor.schoolId;
            await schoolSupervisorRepo.AddSchoolSupervisor(schoolSupervisor_, schoolSupervisor.Password);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(string Id)
        {
            ViewBag.schools = schoolRepo.GetAllSchools();
            var schoolSupervisor_ = schoolSupervisorRepo.GetSchoolSupervisor(Id);
            return View(schoolSupervisor_);
        }
        public IActionResult Edited(SchoolSupervisorDTO schoolSupervisor)
        {
            var schoolSupervisor_ = new SchoolSupervisor();
            schoolSupervisor_.Id = schoolSupervisor.Id;
            schoolSupervisor_.fristName = schoolSupervisor.fristName;
            schoolSupervisor_.secondName = schoolSupervisor.secondName;
            schoolSupervisor_.thirdName = schoolSupervisor.thirdName;
            schoolSupervisor_.lastName = schoolSupervisor.lastName;
            schoolSupervisor_.PhoneNumber = schoolSupervisor.PhoneNumber;
            schoolSupervisor_.schoolId = schoolSupervisor.schoolId;
            schoolSupervisorRepo.EditSchoolSupervisor(schoolSupervisor_);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string Id)
        {
            schoolSupervisorRepo.DeleteSchoolSupervisor(Id);
            return RedirectToAction("Index");
        }

        public IActionResult Trainings()
        
        
        {
            var schoolSupervisorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewBag.trainings = trainingRepo.GetAllTrainings().Where(t => t.schoolSupervisorId == schoolSupervisorId).ToList();
            var schoolSupervisor = schoolSupervisorRepo.GetSchoolSupervisor(schoolSupervisorId);
            return View(schoolSupervisor);
        }
  
		public IActionResult Assignments(int trainingId)
		{
			ViewBag.assignments = assignmentRepo.GetAllAssignments().Where(a => a.trainingId == trainingId).ToList();
			ViewBag.documents = documentRepo.GetAllDocuments();
			return View(trainingId);
		}
		public IActionResult TimeLine(int assignmentId, int trainingId)
		{
			ViewBag.reportsLog = reportsLogRepo.GetAllReportsLogs().OrderByDescending(r => r.logDate).ToList();
			Assignment assignment = assignmentRepo.GetAssignment(assignmentId, trainingId);
			return View(assignment);
		}

	}
}
