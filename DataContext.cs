global using Microsoft.EntityFrameworkCore;

namespace TeacherAPIProject
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>

            optionsBuilder.UseNpgsql("host=localhost; database=teachers;user id=postgres; password=root;");

        public DbSet<Teacher> Teachers => Set<Teacher>();

    }
}
