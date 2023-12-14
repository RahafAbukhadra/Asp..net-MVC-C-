using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Repository
{
	public interface IReportStatusRepository
	{
		public List<ReportStatus> GetAllReportStatuses();
		public void DeleteReportStatus(int reportStatusId);
		public ReportStatus GetReportStatus(int reportStatusId);
		public void EditReportStatus(ReportStatus reportStatus);
		public void AddReportStatus(ReportStatus reportStatus);
	}
}
