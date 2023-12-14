using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Repository
{
	public interface ICompanyRepository
    {
        public List<Company> GetAllCompanies();
        public void DeleteCompany(int companyId);
        public Company GetCompany(int companyId);
        public void EditCompany(Company company);
        public void AddCompany(Company company);
    }
}
