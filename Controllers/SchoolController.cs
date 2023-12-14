
using FinalProject.DTO;
using FinalProject.Entities;
using FinalProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProject.Controllers
{
	public class SchoolController : Controller
    {
        
        ISchoolRepository schoolRepo;
        public SchoolController(ISchoolRepository schoolRepo) {
            this.schoolRepo = schoolRepo;
        }
        public IActionResult Index()
        {
            ViewBag.schools = schoolRepo.GetAllSchools();
            return View();
        }
        public IActionResult Add() {
            return View();
        }
      
        public IActionResult Insert(SchoolDTO school)
		{
			var school_ = new School();
			school_.schoolName = school.schoolName;
			school_.schoolAddress = school.schoolAddress;
			schoolRepo.AddSchool(school_);
			return RedirectToAction("Index");
		}
        public IActionResult Edit(int schoolId)
		{
			var school_ = schoolRepo.GetSchool(schoolId);
			return View(school_);
		}
        
        public IActionResult Edited(SchoolDTO school)
		{
			var school_ = new School();
			school_.schoolId = school.schoolId;
			school_.schoolName = school.schoolName;
			school_.schoolAddress = school.schoolAddress;
			schoolRepo.EditSchool(school_);
			return RedirectToAction("Index");
		}
        
        public IActionResult Delete(int schoolId)
		{
			schoolRepo.DeleteSchool(schoolId);
			return RedirectToAction("Index");

		}
	}
}
