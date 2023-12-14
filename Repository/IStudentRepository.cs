using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Repository
{
	public interface IStudentRepository
    {
        public List<Student> GetAllStudents();
        public void DeleteStudent(string Id);
        public Student GetStudent(string Id);
        public void EditStudent(Student student);
        public Task AddStudent(Student student, string password);
    }
}
