using FinalProject.DTO;
using FinalProject.Entities;
using FinalProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Apprenticeship.Controllers
{
    public class ReportController : Controller
    {
        IReportsLogRepository reportsLogRepo;
        IReportRepository reportRepo;
        IReportStatusRepository reportStatusRepo;
        IStudentRepository studentRepo;
		IDocumentRepository documentRepo;
        ITeamLeaderRepository teamLeaderRepo;
        public ReportController(IReportsLogRepository reportsLogRepo,IReportRepository reportRepo, IDocumentRepository documentRepo, IReportStatusRepository reportStatusRepo, IStudentRepository studentRepo,ITeamLeaderRepository teamLeaderRepo) {
            this.reportsLogRepo = reportsLogRepo;
            this.reportRepo = reportRepo;
			this.reportStatusRepo = reportStatusRepo;
			this.documentRepo = documentRepo;
            this.studentRepo = studentRepo;
            this.teamLeaderRepo = teamLeaderRepo;
        }
        [Authorize(Roles = "TEAMLEADER, STUDENT, SCHOOLSUPERVISOR")]
        public IActionResult Index(int assignmentId)
        {
             ViewBag.reports = reportRepo.GetAllReports().Where(t => t.assignmentId == assignmentId).ToList();
           
            return View(assignmentId);
        }
        public IActionResult Add(int assignmentId)
        {
            ReportDTO report = new ReportDTO();
			report.assignmentId = assignmentId;
            return View(report);
        }
        public IActionResult Insert(ReportDTO report, List<IFormFile> formFile)
        {
            var report_ = new Report();
            report_.reportName = report.reportName;
			report_.reportDescription = report.reportDescription;
			report_.reportNotes = report.reportNotes;
			report_.assignmentId = report.assignmentId;
			report_.reportStatusId = 3;
			reportRepo.AddReport(report_);
            foreach (var form in formFile)
            {
                Document document = new Document();
                if (form.Length > 0)
                {
                    Stream st = form.OpenReadStream();
                    using (BinaryReader br = new BinaryReader(st))
                    {
                        var byteFile = br.ReadBytes((int)st.Length);
                        document.file = byteFile;
                    }
                    document.name = form.FileName;
                    document.contentType = form.ContentType;
                    document.reportId = report_.reportId;
                    documentRepo.AddDocument(document);
                }
            }

            return RedirectToAction("Reports", "Student", new { assignmentId = report.assignmentId });
		}
		public FileStreamResult GetFile(long documentId)
		{
			var file = documentRepo.GetDocument(documentId);
			Stream stream = new MemoryStream(file.file);
			return new FileStreamResult(stream, file.contentType);
		}
        public IActionResult Edit(int reportId)
        {
            var report = reportRepo.GetReport(reportId);
			var report_ = new ReportDTO();
			report_.reportName = report.reportName;
			report_.reportDescription = report.reportDescription;
			report_.reportNotes = report.reportNotes;
			report_.assignmentId = report.assignmentId;
			report_.reportId = report.reportId;
			report_.reportStatusId = report.reportStatusId;
            ViewBag.documents = documentRepo.GetAllDocuments().Where(r => r.reportId == reportId).ToList();
            return View(report_);
        }
     
        public IActionResult Edited(ReportDTO report,List<IFormFile> formFile)
        {
            var report_ = new Report();
			report_.reportName = report.reportName;
			report_.reportDescription = report.reportDescription;
			report_.reportNotes = report.reportNotes;
			report_.reportId = report.reportId;
			report_.reportStatusId = report.reportStatusId;
			reportRepo.EditReport(report_);
            foreach(var form in formFile)
            {
                var reportLog = new ReportsLog();
                reportLog.reportName = report.reportName;
                reportLog.reportDescription = report.reportDescription;
                reportLog.reportNotes = report.reportNotes;
                reportLog.reportId = report.reportId;
                reportLog.reportStatusId = report.reportStatusId;
                reportLog.logDate = DateTime.Now;
                reportLog.reportId = report.reportId;
                reportsLogRepo.AddReportLog(reportLog);
                Document document = new Document();
                if (form.Length > 0)
                {
                    Stream st = form.OpenReadStream();
                    using (BinaryReader br = new BinaryReader(st))
                    {
                        var byteFile = br.ReadBytes((int)st.Length);
                        document.file = byteFile;
                    }
                    document.name = form.FileName;
                    document.contentType = form.ContentType;
                    document.reportId = report_.reportId;
                    document.reportsLogId=reportLog.reportId;
                    documentRepo.AddDocument(document);
                }
            }
            return RedirectToAction("Reports", "Student", new { assignmentId = report.assignmentId });
		}
        public IActionResult EditeReportStatus(ReportDTO report)
		{
			var report_ = new Report();
			report_.reportId= report.reportId;
			report_.reportStatusId= report.reportStatusId;
			reportRepo.EditReportStatusByTeamLeader(report_);

			return RedirectToAction("Reports", "TeamLeader", new { assignmentId = report.assignmentId });
		}
        public IActionResult Delete(ReportDTO report)
        {
            reportRepo.DeleteReport(report.reportId);
			return RedirectToAction("Reports", "Student", new { assignmentId = report.assignmentId });
		}
        public IActionResult DeleteDocument(int reportId, int documentId)
        {
            documentRepo.DeleteDocument(documentId);
            return RedirectToAction("Edit", "Report", new { reportId });
        }
    }
}
