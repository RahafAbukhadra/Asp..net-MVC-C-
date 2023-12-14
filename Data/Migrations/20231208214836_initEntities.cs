using System;
using Microsoft.EntityFrameworkCore.Migrations;


namespace FinalProject.Data.Migrations
{
	public partial class initEntities : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "Discriminator",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<int>(
				name: "Student_schoolId",
				table: "AspNetUsers",
				type: "int",
				nullable: true);

			migrationBuilder.AddColumn<int>(
				name: "companyId",
				table: "AspNetUsers",
				type: "int",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "fristName",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<string>(
				name: "lastName",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<int>(
				name: "schoolId",
				table: "AspNetUsers",
				type: "int",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "secondName",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<string>(
				name: "studentMajor",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "thirdName",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "");

			migrationBuilder.CreateTable(
				name: "companies",
				columns: table => new
				{
					companyId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					companyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					companyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_companies", x => x.companyId);
				});

			migrationBuilder.CreateTable(
				name: "objectives",
				columns: table => new
				{
					objectiveId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					objectiveName = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_objectives", x => x.objectiveId);
				});

			migrationBuilder.CreateTable(
				name: "reportStatuses",
				columns: table => new
				{
					reportStatusId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					reportStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_reportStatuses", x => x.reportStatusId);
				});

			migrationBuilder.CreateTable(
				name: "schools",
				columns: table => new
				{
					schoolId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					schoolName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					schoolAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_schools", x => x.schoolId);
				});

			migrationBuilder.CreateTable(
				name: "skills",
				columns: table => new
				{
					skillId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					skillName = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_skills", x => x.skillId);
				});

			migrationBuilder.CreateTable(
				name: "trainings",
				columns: table => new
				{
					trainingId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					studentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					teamLeaderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					schoolSupervisorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					endDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_trainings", x => x.trainingId);
					table.ForeignKey(
						name: "FK_trainings_AspNetUsers_schoolSupervisorId",
						column: x => x.schoolSupervisorId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_trainings_AspNetUsers_studentId",
						column: x => x.studentId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_trainings_AspNetUsers_teamLeaderId",
						column: x => x.teamLeaderId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "objectiveSkills",
				columns: table => new
				{
					objectiveId = table.Column<int>(type: "int", nullable: false),
					skillId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_objectiveSkills", x => new { x.objectiveId, x.skillId });
					table.ForeignKey(
						name: "FK_objectiveSkills_objectives_objectiveId",
						column: x => x.objectiveId,
						principalTable: "objectives",
						principalColumn: "objectiveId",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_objectiveSkills_skills_skillId",
						column: x => x.skillId,
						principalTable: "skills",
						principalColumn: "skillId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "assignments",
				columns: table => new
				{
					assignmentId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					assignmentTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
					assignmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
					assignmentNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
					startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					endDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					trainingId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_assignments", x => x.assignmentId);
					table.ForeignKey(
						name: "FK_assignments_trainings_trainingId",
						column: x => x.trainingId,
						principalTable: "trainings",
						principalColumn: "trainingId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "trainingObjectives",
				columns: table => new
				{
					trainingId = table.Column<int>(type: "int", nullable: false),
					objectiveId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_trainingObjectives", x => new { x.trainingId, x.objectiveId });
					table.ForeignKey(
						name: "FK_trainingObjectives_objectives_objectiveId",
						column: x => x.objectiveId,
						principalTable: "objectives",
						principalColumn: "objectiveId",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_trainingObjectives_trainings_trainingId",
						column: x => x.trainingId,
						principalTable: "trainings",
						principalColumn: "trainingId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "assignmentObjectives",
				columns: table => new
				{
					assignmentId = table.Column<int>(type: "int", nullable: false),
					objectiveId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_assignmentObjectives", x => new { x.assignmentId, x.objectiveId });
					table.ForeignKey(
						name: "FK_assignmentObjectives_assignments_assignmentId",
						column: x => x.assignmentId,
						principalTable: "assignments",
						principalColumn: "assignmentId",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_assignmentObjectives_objectives_objectiveId",
						column: x => x.objectiveId,
						principalTable: "objectives",
						principalColumn: "objectiveId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "reports",
				columns: table => new
				{
					reportId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					reportName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					reportDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
					reportNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
					assignmentId = table.Column<int>(type: "int", nullable: false),
					reportStatusId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_reports", x => x.reportId);
					table.ForeignKey(
						name: "FK_reports_assignments_assignmentId",
						column: x => x.assignmentId,
						principalTable: "assignments",
						principalColumn: "assignmentId",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_reports_reportStatuses_reportStatusId",
						column: x => x.reportStatusId,
						principalTable: "reportStatuses",
						principalColumn: "reportStatusId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "reportsLogs",
				columns: table => new
				{
					reportLogId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					reportId = table.Column<int>(type: "int", nullable: false),
					reportName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					reportDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
					reportNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
					reportStatusId = table.Column<int>(type: "int", nullable: false),
					logDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_reportsLogs", x => x.reportLogId);
					table.ForeignKey(
						name: "FK_reportsLogs_reports_reportId",
						column: x => x.reportId,
						principalTable: "reports",
						principalColumn: "reportId",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_reportsLogs_reportStatuses_reportStatusId",
						column: x => x.reportStatusId,
						principalTable: "reportStatuses",
						principalColumn: "reportStatusId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "documents",
				columns: table => new
				{
					documentId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					contentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
					file = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
					reportId = table.Column<int>(type: "int", nullable: true),
					assignmentId = table.Column<int>(type: "int", nullable: true),
					reportsLogId = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_documents", x => x.documentId);
					table.ForeignKey(
						name: "FK_documents_assignments_assignmentId",
						column: x => x.assignmentId,
						principalTable: "assignments",
						principalColumn: "assignmentId");
					table.ForeignKey(
						name: "FK_documents_reports_reportId",
						column: x => x.reportId,
						principalTable: "reports",
						principalColumn: "reportId");
					table.ForeignKey(
						name: "FK_documents_reportsLogs_reportsLogId",
						column: x => x.reportsLogId,
						principalTable: "reportsLogs",
						principalColumn: "reportLogId");
				});

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUsers_companyId",
				table: "AspNetUsers",
				column: "companyId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUsers_schoolId",
				table: "AspNetUsers",
				column: "schoolId");

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUsers_Student_schoolId",
				table: "AspNetUsers",
				column: "Student_schoolId");

			migrationBuilder.CreateIndex(
				name: "IX_assignmentObjectives_objectiveId",
				table: "assignmentObjectives",
				column: "objectiveId");

			migrationBuilder.CreateIndex(
				name: "IX_assignments_trainingId",
				table: "assignments",
				column: "trainingId");

			migrationBuilder.CreateIndex(
				name: "IX_documents_assignmentId",
				table: "documents",
				column: "assignmentId",
				unique: true,
				filter: "[assignmentId] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_documents_reportId",
				table: "documents",
				column: "reportId",
				unique: true,
				filter: "[reportId] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_documents_reportsLogId",
				table: "documents",
				column: "reportsLogId",
				unique: true,
				filter: "[reportsLogId] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_objectiveSkills_skillId",
				table: "objectiveSkills",
				column: "skillId");

			migrationBuilder.CreateIndex(
				name: "IX_reports_assignmentId",
				table: "reports",
				column: "assignmentId");

			migrationBuilder.CreateIndex(
				name: "IX_reports_reportStatusId",
				table: "reports",
				column: "reportStatusId");

			migrationBuilder.CreateIndex(
				name: "IX_reportsLogs_reportId",
				table: "reportsLogs",
				column: "reportId");

			migrationBuilder.CreateIndex(
				name: "IX_reportsLogs_reportStatusId",
				table: "reportsLogs",
				column: "reportStatusId");

			migrationBuilder.CreateIndex(
				name: "IX_trainingObjectives_objectiveId",
				table: "trainingObjectives",
				column: "objectiveId");

			migrationBuilder.CreateIndex(
				name: "IX_trainings_schoolSupervisorId",
				table: "trainings",
				column: "schoolSupervisorId");

			migrationBuilder.CreateIndex(
				name: "IX_trainings_studentId",
				table: "trainings",
				column: "studentId");

			migrationBuilder.CreateIndex(
				name: "IX_trainings_teamLeaderId",
				table: "trainings",
				column: "teamLeaderId");

			migrationBuilder.AddForeignKey(
				name: "FK_AspNetUsers_companies_companyId",
				table: "AspNetUsers",
				column: "companyId",
				principalTable: "companies",
				principalColumn: "companyId",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_AspNetUsers_schools_schoolId",
				table: "AspNetUsers",
				column: "schoolId",
				principalTable: "schools",
				principalColumn: "schoolId",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_AspNetUsers_schools_Student_schoolId",
				table: "AspNetUsers",
				column: "Student_schoolId",
				principalTable: "schools",
				principalColumn: "schoolId",
				onDelete: ReferentialAction.Restrict);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_AspNetUsers_companies_companyId",
				table: "AspNetUsers");

			migrationBuilder.DropForeignKey(
				name: "FK_AspNetUsers_schools_schoolId",
				table: "AspNetUsers");

			migrationBuilder.DropForeignKey(
				name: "FK_AspNetUsers_schools_Student_schoolId",
				table: "AspNetUsers");

			migrationBuilder.DropTable(
				name: "assignmentObjectives");

			migrationBuilder.DropTable(
				name: "companies");

			migrationBuilder.DropTable(
				name: "documents");

			migrationBuilder.DropTable(
				name: "objectiveSkills");

			migrationBuilder.DropTable(
				name: "schools");

			migrationBuilder.DropTable(
				name: "trainingObjectives");

			migrationBuilder.DropTable(
				name: "reportsLogs");

			migrationBuilder.DropTable(
				name: "skills");

			migrationBuilder.DropTable(
				name: "objectives");

			migrationBuilder.DropTable(
				name: "reports");

			migrationBuilder.DropTable(
				name: "assignments");

			migrationBuilder.DropTable(
				name: "reportStatuses");

			migrationBuilder.DropTable(
				name: "trainings");

			migrationBuilder.DropIndex(
				name: "IX_AspNetUsers_companyId",
				table: "AspNetUsers");

			migrationBuilder.DropIndex(
				name: "IX_AspNetUsers_schoolId",
				table: "AspNetUsers");

			migrationBuilder.DropIndex(
				name: "IX_AspNetUsers_Student_schoolId",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "Discriminator",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "Student_schoolId",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "companyId",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "fristName",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "lastName",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "schoolId",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "secondName",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "studentMajor",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "thirdName",
				table: "AspNetUsers");
		}
	}
}
