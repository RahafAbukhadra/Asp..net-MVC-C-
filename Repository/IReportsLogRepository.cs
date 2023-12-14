using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Repository
{
	public interface IReportsLogRepository
	{
		public List<ReportsLog> GetAllReportsLogs();
		public void DeleteReportLog(int reportLogId);
		public ReportsLog GetReportLog(int reportLogId);
		public void EditReportLog(ReportsLog reportLog);
		public void AddReportLog(ReportsLog reportLog);
	}
}
