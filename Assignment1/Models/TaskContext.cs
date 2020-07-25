using Microsoft.EntityFrameworkCore;

namespace TasksWebService.Models
{
    public class TaskContext : DbContext
    {

        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }

        public DbSet<Tasks> TaskItems { get; set; }
    }
}
