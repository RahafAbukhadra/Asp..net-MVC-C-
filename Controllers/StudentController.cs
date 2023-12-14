using FinalProject.DTO;
using FinalProject.Entities;
using FinalProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Apprenticeship.Controllers
{
    public class StudentController : Controller
    {
		IDocumentRepository documentRepo;
		IStudentRepository studentRepo;
		ISchoolRepository schoolRepo;
		IAssignmentRepository assignmentsRepo;
		ITrainingRepository trainingRepo;
		IReportRepository reportRepo;
		public StudentController(IStudentRepository studentRepo, IDocumentRepository documentRepo, ISchoolRepository schoolRepo, ITrainingRepository trainingRepo, IAssignmentRepository assignmentsRepo, IReportRepository reportRepo)
        {
            this.documentRepo = documentRepo;
            this.studentRepo = studentRepo;
            this.schoolRepo = schoolRepo;
			this.trainingRepo = trainingRepo;
            this.assignmentsRepo = assignmentsRepo;
			this.reportRepo = reportRepo;
        }
        [Authorize(Roles = "ADMIN,TEAMLEADER")]
        public IActionResult Index()
        {
            ViewBag.Students = studentRepo.GetAllStudents();
            return View();
        }
        public IActionResult Add() {
            ViewBag.schools = schoolRepo.GetAllSchools();
            return View();
        }
        public async Task<IActionResult> Insert(StudentDTO student)
		{
			var student_ = new Student();
            student_.fristName = student.fristName;
            student_.secondName = student.secondName;
            student_.thirdName = student.thirdName;
            student_.lastName = student.lastName;
            student_.schoolId = student.schoolId;
            student_.PhoneNumber = student.PhoneNumber;
            student_.Email = student.Email;
            student_.NormalizedEmail = student.Email.ToUpper();
            student_.UserName = student.Email;
            student_.NormalizedUserName = student.Email.ToUpper();
            student_.studentMajor = student.studentMajor;
            student_.schoolId = student.schoolId;
            await studentRepo.AddStudent(student_,student.Password);
			return RedirectToAction("Index");
		}
        public IActionResult Edit(string Id)
		{
            ViewBag.schools = schoolRepo.GetAllSchools();
            var student_ = studentRepo.GetStudent(Id);
			return View(student_);
		}
        public IActionResult Edited(StudentDTO student)
		{
			var student_ = new Student();
			student_.Id = student.Id;
			student_.fristName = student.fristName;
			student_.secondName = student.secondName;
			student_.thirdName = student.thirdName;
			student_.lastName = student.lastName;
            student_.schoolId = student.schoolId;
            student_.PhoneNumber = student.PhoneNumber;
            student_.studentMajor = student.studentMajor;
            student_.schoolId = student.schoolId;
            studentRepo.EditStudent(student_);
			return RedirectToAction("Index");
		}

        public IActionResult Delete(string Id)
		{
			studentRepo.DeleteStudent(Id);
			return RedirectToAction("Index");
		}
        public IActionResult Trainings()
		{
			var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			ViewBag.trainings = trainingRepo.GetAllTrainings().Where(t => t.studentId == studentId).ToList();
			var student = studentRepo.GetStudent(studentId);
			return View(student);
		}
       
        public IActionResult Assignments(int trainingId)
		{
			ViewBag.assignments = assignmentsRepo.GetAllAssignments().Where(t => t.trainingId == trainingId).ToList();
			ViewBag.documents = documentRepo.GetAllDocuments();
			return View();
		}
     
        public IActionResult Reports(int assignmentId)
		{
			ViewBag.reports = reportRepo.GetAllReports().Where(r => r.assignmentId == assignmentId).ToList();
			ViewBag.documents = documentRepo.GetAllDocuments();
			return View(assignmentId);
		}
	}
}
