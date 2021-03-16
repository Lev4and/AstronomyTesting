using System;
using AstronomyTesting.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AstronomyTesting.Model
{
    public partial class AstronomyTestingContext : DbContext
    {
        public AstronomyTestingContext()
        {
        }

        public AstronomyTestingContext(DbContextOptions<AstronomyTestingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VDetailsOfTheStudentPassingTheTest> VDetailsOfTheStudentPassingTheTests { get; set; }
        public virtual DbSet<VDetailsOfTheTest> VDetailsOfTheTests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-9CDGA5B;Database=AstronomyTesting;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("answer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsCorrect).HasColumnName("is_correct");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_answer_question");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("group");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("question");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("name");

                entity.Property(e => e.Photo).HasColumnName("photo");

                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("FK_question_test");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_student_group");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_student_user");
            });

            modelBuilder.Entity<StudentAnswer>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.AnswerId })
                    .HasName("PK_user_answer");

                entity.ToTable("student_answer");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.AnswerId).HasColumnName("answer_id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.StudentAnswers)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("FK_student_answer_answer");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentAnswers)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_student_answer_student");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("test");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateOfCreating)
                    .HasColumnType("datetime")
                    .HasColumnName("date_of_creating");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiration_date");

                entity.Property(e => e.MaximumNumberOfQuestions).HasColumnName("maximum_number_of_questions");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("full_name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_user_role");
            });

            modelBuilder.Entity<VDetailsOfTheStudentPassingTheTest>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_details_of_the_student_passing_the_test");

                entity.Property(e => e.AnswerId).HasColumnName("answer_id");

                entity.Property(e => e.AnswerIsCorrect).HasColumnName("answer_is_correct");

                entity.Property(e => e.AnswerName)
                    .IsRequired()
                    .HasColumnName("answer_name");

                entity.Property(e => e.CorrectAnswer).HasColumnName("correct_answer");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.QuestionName)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("question_name");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.StudentName)
                    .IsRequired()
                    .HasColumnName("student_name");

                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.Property(e => e.TestName)
                    .IsRequired()
                    .HasColumnName("test_name");
            });

            modelBuilder.Entity<VDetailsOfTheTest>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_details_of_the_test");

                entity.Property(e => e.CountAnswers).HasColumnName("count_answers");

                entity.Property(e => e.CountCorrectedAnswers).HasColumnName("count_corrected_answers");

                entity.Property(e => e.CountQuestions).HasColumnName("count_questions");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasColumnName("group_name");

                entity.Property(e => e.MaximumNumberOfQuestions).HasColumnName("maximum_number_of_questions");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.StudentName)
                    .IsRequired()
                    .HasColumnName("student_name");

                entity.Property(e => e.TestDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("test_date_time");

                entity.Property(e => e.TestId).HasColumnName("test_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
