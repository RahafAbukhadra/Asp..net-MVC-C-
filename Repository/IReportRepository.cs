using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Repository
{
	public interface IReportRepository
    { 
            public List<Report> GetAllReports();
            public void DeleteReport(int reportId);
            public Report GetReport(int reportId);
            public void EditReport(Report report);
            public void AddReport(Report report);
            public void EditReportStatusByTeamLeader(Report report);

	}
}
