using Microsoft.EntityFrameworkCore;
using ModelEntity.Model;

namespace Entitys.Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=MyDatabase.db");
        }
        public DbSet<Level> Levels { get; set; }
        public DbSet<LevelDescriptor> LevelsDescriptors { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionAnswer> QuestionsAnswers { get; set; }
        public DbSet<QuestionDescriptor> QuestionsDescriptors { get; set; }
    }
}
