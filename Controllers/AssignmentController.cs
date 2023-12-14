using FinalProject.DTO;
using FinalProject.Entities;
using FinalProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace Apprenticeship.Controllers
{
    public class AssignmentController : Controller
    {
        IAssignmentRepository  assignmentRepo;
        IDocumentRepository documentRepo;
        IObjectiveRepository objectiveRepo;
        IAssignmentObjectivesRepository assignmentObjectivesRepo;
        ITrainingObjectivesRepository trainingObjectivesRepo;
        IStudentRepository studentRepo;
        ITeamLeaderRepository teamLeaderRepo;
        public AssignmentController(IAssignmentRepository assignmentRepo, IDocumentRepository documentRepo, IObjectiveRepository objectiveRepo, IAssignmentObjectivesRepository assignmentObjectivesRepo, ITrainingObjectivesRepository trainingObjectivesRepo, ITeamLeaderRepository teamLeaderRepo)
        {
            this.assignmentRepo = assignmentRepo;
            this.documentRepo = documentRepo;   
            this.objectiveRepo = objectiveRepo;
            this.assignmentObjectivesRepo = assignmentObjectivesRepo;
            this.trainingObjectivesRepo = trainingObjectivesRepo;
            this.studentRepo = studentRepo;
            this.teamLeaderRepo = teamLeaderRepo;
        }
        [Authorize(Roles = "TEAMLEADER,STUDENT, SCHOOLSUPERVISOR")]
        public IActionResult Index(int trainingId)
		{
			ViewBag.assignments = assignmentRepo.GetAllAssignments();
            return View(trainingId);
		}
      
        public IActionResult Add(int trainingId)
        {
			AssignmentDTO assignment=new AssignmentDTO();
            assignment.trainingId =trainingId;
            ViewBag.objectives = trainingObjectivesRepo.GetAllTrainingObjectives().Where(t=>t.trainingId == assignment.trainingId).ToList();
			return View(assignment);
        }
       
        public IActionResult Insert(AssignmentDTO assignment, List<IFormFile> formFile)
        {
            var assignment_ = new Assignment();
            assignment_.assignmentTitle = assignment.assignmentTitle;
            assignment_.assignmentDescription = assignment.assignmentDescription;
            assignment_.assignmentNotes = assignment.assignmentNotes;
			assignment_.startDate = assignment.startDate;
            assignment_.endDate = assignment.endDate;
            assignment_.trainingId = assignment.trainingId;
            assignment_.assignmentId = assignment.assignmentId;
			assignmentRepo.AddAssignment(assignment_,assignment.objectiveIds);
            var assignmentId = assignment_.assignmentId;
            foreach(var file in formFile) { 
                Document document = new Document();
                if (file.Length > 0)
                {
                    Stream st = file.OpenReadStream();
                    using (BinaryReader br = new BinaryReader(st))
                    {
                        var byteFile = br.ReadBytes((int)st.Length);
                        document.file = byteFile;
                    }
                    document.name = file.FileName;
                    document.contentType = file.ContentType;
                    document.assignmentId= assignmentId;
                    documentRepo.AddDocument(document);
                }
            }
            return RedirectToAction("Assignments", "TeamLeader", new { trainingId = assignment.trainingId });
		}
		
		public FileStreamResult GetFile(long documentId)
		{
            var file = documentRepo.GetDocument(documentId);
			Stream stream = new MemoryStream(file.file);
			return new FileStreamResult(stream, file.contentType);
		}
		
        public IActionResult Edit(int assignmentId, int trainingId)
        {
            var assignment = assignmentRepo.GetAssignment(assignmentId, trainingId);
            AssignmentDTO assignment_ = new AssignmentDTO();
            assignment_.assignmentTitle = assignment.assignmentTitle;
            assignment_.assignmentDescription = assignment.assignmentDescription;
            assignment_.assignmentNotes = assignment.assignmentNotes;
            assignment_.startDate = assignment.startDate;
            assignment_.endDate = assignment.endDate;
            assignment_.trainingId = assignment.trainingId;
            assignment_.assignmentId = assignment.assignmentId;
            ViewBag.assignmentObjectivesSelected = assignmentObjectivesRepo.GetAllAssignmentObjectives().Where(t => t.assignmentId == assignmentId).ToList();
            ViewBag.assignmentObjectives = trainingObjectivesRepo.GetAllTrainingObjectives().Where(t => t.trainingId == trainingId).ToList();
            ViewBag.documents = documentRepo.GetAllDocuments().Where(a => a.assignmentId == assignmentId).ToList();
			return View(assignment_);
        }
       
        public IActionResult Edited(AssignmentDTO assignment,List<IFormFile> formFile)
        {
			var assignment_ = new Assignment();
			assignment_.assignmentId = assignment.assignmentId;
			assignment_.assignmentTitle = assignment.assignmentTitle;
			assignment_.assignmentDescription = assignment.assignmentDescription;
			assignment_.assignmentNotes = assignment.assignmentNotes;
			assignment_.startDate = assignment.startDate;
			assignment_.endDate = assignment.endDate;
			assignment_.trainingId = assignment.trainingId;
			assignmentRepo.EditAssignment(assignment_);
            assignmentObjectivesRepo.RemoveAssignmentObjectivesByAssignmentId(assignment.assignmentId);
            foreach (var o in assignment.objectiveIds)
            {
                assignmentObjectivesRepo.AddAssignmentObjective(new AssignmentObjectives { assignmentId = assignment.assignmentId, objectiveId = o });
            }
            foreach (var file in formFile)
            {
                Document document = new Document();
                if (file.Length > 0)
                {
                    Stream st = file.OpenReadStream();
                    using (BinaryReader br = new BinaryReader(st))
                    {
                        var byteFile = br.ReadBytes((int)st.Length);
                        document.file = byteFile;
                    }
                    document.name = file.FileName;
                    document.contentType = file.ContentType;
                    document.assignmentId = assignment_.assignmentId;
                    documentRepo.AddDocument(document);
                }
            }

            return RedirectToAction("Assignments", "TeamLeader", new { trainingId = assignment.trainingId });
		}
        public IActionResult Delete(AssignmentDTO assignment)
        {
            assignmentRepo.DeleteAssignment(assignment.assignmentId,assignment.trainingId);
			return RedirectToAction("Assignments", "TeamLeader", new { trainingId = assignment.trainingId });
		}
        public IActionResult DeleteDocument(int assignmentId,int trainingId, int documentId)
        {
            documentRepo.DeleteDocument(documentId);
            return RedirectToAction("Edit", "Assignment", new {assignmentId,trainingId});
        }
    }
}
    