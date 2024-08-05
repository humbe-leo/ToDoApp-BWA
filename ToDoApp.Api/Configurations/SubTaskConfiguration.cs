using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.Api.Models;

namespace ToDoApp.Api.Configurations
{
	public class SubTaskConfiguration : IEntityTypeConfiguration<SubTask>
	{
		public void Configure(EntityTypeBuilder<SubTask> builder)
		{
			builder.ToTable("SubTasks");

			builder.HasKey(e => e.SubTaskId);
			builder.Property(e => e.SubTaskId).ValueGeneratedOnAdd();

			builder.Property(e => e.Title)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(e => e.DateCreated)
				.IsRequired();

			builder.Property(e => e.Status)
				.IsRequired();

			builder.Property(e => e.IsImportant)
				.IsRequired();

			builder.HasOne(e => e.Goal)
				.WithMany(e => e.SubTasks)
				.HasForeignKey(e => e.GoalId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
