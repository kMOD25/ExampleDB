using Microsoft.EntityFrameworkCore;
using ModelEntity.Model;

namespace Entitys.ModelEntity
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Server=(localdb)\mssqllocaldb;Database=NameDb;Trusted_Connection=True;");
        }
        public DbSet<Levels> Levels { get; set; }
        public DbSet<LevelsDescriptors> LevelsDescriptors { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<QuestionsAnswers> QuestionsAnswers { get; set; }
        public DbSet<QuestionsDescriptors> QuestionsDescriptors { get; set; }
    }
}
