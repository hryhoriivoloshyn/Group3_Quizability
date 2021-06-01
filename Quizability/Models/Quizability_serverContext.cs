using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Quizability.Models
{
    public partial class Quizability_serverContext : DbContext
    {
        public Quizability_serverContext()
        {
        }

        public Quizability_serverContext(DbContextOptions<Quizability_serverContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Quize> Quizes { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAchievement> UserAchievements { get; set; }
        public virtual DbSet<UserQuiz> UserQuizzes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=qserver-group3.database.windows.net;Initial Catalog=Quizability_server;Persist Security Info=False;User ID=qadmin;Password=nureNure2021;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Achievement>(entity =>
            {
                entity.Property(e => e.AchievementId).HasColumnName("achievement_id");

                entity.Property(e => e.AchievementName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("achievement_name");

                entity.Property(e => e.AchievementText).HasColumnName("achievement_text");

                entity.Property(e => e.QuizId).HasColumnName("quiz_id");
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.AnswerId).HasColumnName("answer_id");

                entity.Property(e => e.AnswerText).HasColumnName("answer_text");

                entity.Property(e => e.Correct).HasColumnName("correct");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answers_Questions");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.ImageSrc)
                    .HasMaxLength(255)
                    .HasColumnName("image_src");

                entity.Property(e => e.QuestionPoints).HasColumnName("question_points");

                entity.Property(e => e.QuestionText).HasColumnName("question_text");

                entity.Property(e => e.QuestionType).HasColumnName("question_type");

                entity.Property(e => e.QuizId).HasColumnName("quiz_id");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK_Questions_Quizes");
            });

            modelBuilder.Entity<Quize>(entity =>
            {
                entity.HasKey(e => e.QuizId);

                entity.Property(e => e.QuizId).HasColumnName("quiz_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Difficulty).HasColumnName("difficulty");

                entity.Property(e => e.ImageSrc)
                    .HasMaxLength(255)
                    .HasColumnName("image_src");

                entity.Property(e => e.Popular).HasColumnName("popular");

                entity.Property(e => e.QuizName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("quiz_name");

                entity.Property(e => e.QuizTime).HasColumnName("quiz_time");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.TopicId).HasColumnName("topic_id");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Quizes)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Quizes_Topics");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("status_name");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.Property(e => e.TopicId).HasColumnName("topic_id");

                entity.Property(e => e.TopicName)
                    .HasMaxLength(255)
                    .HasColumnName("topic_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.StatusId).HasColumnName("status_id");
            });

            modelBuilder.Entity<UserAchievement>(entity =>
            {
                entity.HasKey(e => new { e.AchievementId, e.UserId });

                entity.ToTable("User_Achievement");

                entity.Property(e => e.AchievementId).HasColumnName("achievement_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.ObtainingTime)
                    .HasColumnType("datetime")
                    .HasColumnName("obtaining_time");

                entity.HasOne(d => d.Achievement)
                    .WithMany(p => p.UserAchievements)
                    .HasForeignKey(d => d.AchievementId)
                    .HasConstraintName("FK_User_Achievement_Users");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAchievements)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_Achievement_Users1");
            });

            modelBuilder.Entity<UserQuiz>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.QuizId, e.StartTme });

                entity.ToTable("User_Quiz");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.QuizId).HasColumnName("quizId");

                entity.Property(e => e.StartTme)
                    .HasColumnType("datetime")
                    .HasColumnName("startTme");

                entity.Property(e => e.FinishTime)
                    .HasColumnType("datetime")
                    .HasColumnName("finishTime");

                entity.Property(e => e.Finished).HasColumnName("finished");

                entity.Property(e => e.Points).HasColumnName("points");

                entity.Property(e => e.RightAnswersAmount).HasColumnName("rightAnswersAmount");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.UserQuizzes)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK_User_Quiz_Quizes");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserQuizzes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_Quiz_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
