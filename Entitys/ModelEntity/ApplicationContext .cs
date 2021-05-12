using Microsoft.EntityFrameworkCore;
using ModelEntity.Model;

namespace Entitys.ModelEntity
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=MyDatabase.db");
        }
        public DbSet<Levels> Levels { get; set; }
        public DbSet<LevelsDescriptors> LevelsDescriptors { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<QuestionsAnswers> QuestionsAnswers { get; set; }
        public DbSet<QuestionsDescriptors> QuestionsDescriptors { get; set; }
    }
}
