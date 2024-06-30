using AreaDoAluno.Models;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Data
{
    public class DataContext : DbContext 
    {
        //public DataContext(DbContextOptions options) : base (options) {}
        public DbSet<Adress> Adress { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Discipline> Discipline { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Exam> Exam{ get; set; }
        public DbSet<Materials> Materials { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<StudentExam> StudentExam { get; set; }
        public DbSet<StudentClass> StudentClass { get; set; }
        public DbSet<Tuition> Tuition { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=localhost;Port=3306;Database=AreaDoAluno;User=root;Password=hendrew;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Professor>().ToTable("Professors");
        }


    }
}