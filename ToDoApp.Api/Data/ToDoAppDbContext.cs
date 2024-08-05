using Microsoft.EntityFrameworkCore;
using ToDoApp.Api.Configurations;
using ToDoApp.Api.Models;

namespace ToDoApp.Api.Data
{
	public class ToDoAppDbContext : DbContext
	{
		#region === DB Sets ===
		public DbSet<Goal> Goals { get; set; }
		public DbSet<SubTask> SubTasks { get; set; }
		#endregion

		public ToDoAppDbContext(DbContextOptions<ToDoAppDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new GoalConfiguration());
			modelBuilder.ApplyConfiguration(new SubTaskConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
