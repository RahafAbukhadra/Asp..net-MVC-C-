
using FinalProject.DTO;
using FinalProject.Entities;
using FinalProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProject.Controllers
{
	public class CompanyController : Controller
    {
        ICompanyRepository companyRepo;
        public CompanyController(ICompanyRepository companyRepo) {
            this.companyRepo = companyRepo;
        }
        public IActionResult Index()
        {
            ViewBag.companies = companyRepo.GetAllCompanies();
            return View();
        }
        public IActionResult Add() {
            return View();
        }
        public IActionResult Insert(CompanyDTO company)
		{
			var company_ = new Company();
			company_.companyName = company.companyName;
			company_.companyAddress = company.companyAddress;
			companyRepo.AddCompany(company_);
			return RedirectToAction("Index");
		}
        public IActionResult Edit(int companyId)
		{
			var company_ = companyRepo.GetCompany(companyId);
			return View(company_);
		}
      
        public IActionResult Edited(CompanyDTO company)
		{
			var company_ = new Company();
			company_.companyId = company.companyId;
			company_.companyName = company.companyName;
			company_.companyAddress = company.companyAddress;
			companyRepo.EditCompany(company_);
			return RedirectToAction("Index");
		}
   
        public IActionResult Delete(int companyId)
		{
			companyRepo.DeleteCompany(companyId);
			return RedirectToAction("Index");

		}
	}
}
