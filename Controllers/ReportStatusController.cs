
using FinalProject.DTO;
using FinalProject.Entities;
using FinalProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProject.Controllers
{
	[Authorize(Roles = "SCHOOLSUPERVISOR,TEAMLEADER")]
    public class ReportStatusController : Controller
	{
		IReportStatusRepository reportStatusRepo;
		public ReportStatusController(IReportStatusRepository reportStatusRepo)
		{
			this.reportStatusRepo = reportStatusRepo;
		}
		//Read From Database
		public IActionResult Index()
		{
			ViewBag.reportStatuses = reportStatusRepo.GetAllReportStatuses();
			return View();
		}

		public IActionResult Add()
		{
			return View();
		}
		//Create ReportStatus Row in Database
		public IActionResult Insert(ReportStatusDTO reportStatus)
		{
			var reportStatus_ = new ReportStatus();
			reportStatus_.reportStatusName = reportStatus.reportStatusName;
			reportStatusRepo.AddReportStatus(reportStatus_);
			return RedirectToAction("Index");
		}

		public IActionResult Edit(int reportStatusId)
		{
			var reportStatus_ = reportStatusRepo.GetReportStatus(reportStatusId);
			return View(reportStatus_);
		}
		//Edit ReportStatus Row in Database
		public IActionResult Edited(ReportStatusDTO reportStatus)
		{
			var reportStatus_ = new ReportStatus();
			reportStatus_.reportStatusId = reportStatus.reportStatusId;
			reportStatus_.reportStatusName = reportStatus.reportStatusName;


			reportStatusRepo.EditReportStatus(reportStatus_);
			return RedirectToAction("Index");
		}
		//Delete ReportStatus Row in Database
		public IActionResult Delete(int reportStatusId)
		{
			reportStatusRepo.DeleteReportStatus(reportStatusId);
			return RedirectToAction("Index");
		}
	}
}
