using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.Api.Models;

namespace ToDoApp.Api.Configurations
{
	public class GoalConfiguration : IEntityTypeConfiguration<Goal>
	{
		public void Configure(EntityTypeBuilder<Goal> builder)
		{
			builder.ToTable("Goals");

			builder.HasKey(e => e.GoalId);
			builder.Property(e => e.GoalId).ValueGeneratedOnAdd();

			builder.Property(e => e.Title)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(e => e.DateCreated)
				.IsRequired();

			builder.Property(e => e.Progress)
				.HasColumnType("decimal(5,2)")
				.HasDefaultValue(0);

			builder.HasMany(e => e.SubTasks)
				.WithOne(e => e.Goal)
				.HasForeignKey(e => e.GoalId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
