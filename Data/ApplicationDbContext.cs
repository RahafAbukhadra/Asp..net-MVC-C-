using FinalProject.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Data
{
	public class ApplicationDbContext : IdentityDbContext<Person>
	{
		public DbSet<Student> students { get; set; }
		public DbSet<TeamLeader> teamLeaders { get; set; }
		public DbSet<SchoolSupervisor> schoolSupervisors { get; set; }
		public DbSet<School> schools { get; set; }
		public DbSet<Company> companies { get; set; }
		public DbSet<Training> trainings { get; set; }
		public DbSet<Objective> objectives { get; set; }
		public DbSet<TrainingObjectives> trainingObjectives { get; set; }
		public DbSet<Skill> skills { get; set; }
		public DbSet<ObjectiveSkills> objectiveSkills { get; set; }
		public DbSet<Report> reports { get; set; }
		public DbSet<ReportStatus> reportStatuses { get; set; }
		public DbSet<Assignment> assignments { get; set; }
		public DbSet<AssignmentObjectives> assignmentObjectives { get; set; }
		public DbSet<ReportsLog> reportsLogs { get; set; }
		public DbSet<Document> documents { get; set; }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Training>()
				.HasKey(t => t.trainingId);

			builder.Entity<ReportsLog>()
				.HasKey(r => r.reportLogId);

			builder.Entity<ObjectiveSkills>()
				.HasKey(os => new { os.objectiveId, os.skillId });

			builder.Entity<TrainingObjectives>()
				.HasKey(os => new { os.trainingId, os.objectiveId });

			builder.Entity<AssignmentObjectives>()
				.HasKey(os => new { os.assignmentId, os.objectiveId });

		}
	}
}
