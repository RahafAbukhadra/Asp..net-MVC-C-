using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Repository
{
	public interface ISchoolSupervisorRepository
	{
		public List<SchoolSupervisor> GetAllSchoolSupervisors();
		public void DeleteSchoolSupervisor(string Id);
		public SchoolSupervisor GetSchoolSupervisor(string Id);
		public void EditSchoolSupervisor(SchoolSupervisor schoolSupervisor);
		public Task AddSchoolSupervisor(SchoolSupervisor schoolSupervisor, string password);
	}
}
