using System.ComponentModel.DataAnnotations;
using ToDoApp.Client.Models;
using ToDoApp.Client.Services;

namespace ToDoApp.Client.Validators
{
	public class UniqueValueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
			if (validationContext.ObjectType == typeof(Goal))
			{
				var goalService = validationContext.GetService<IGoalService>() ?? throw new InvalidOperationException("IGoalService is not registered in the DI container");
				if (!goalService.IsTitleUnique((string)value))
				{
					return new ValidationResult(ErrorMessage ?? "Esta meta ya se encuentra registrada.");
				}
			}
			else if (validationContext.ObjectType == typeof(SubTask))
			{
				var subtaskService = validationContext.GetService<ISubTaskService>() ?? throw new InvalidOperationException("ISubTaskService is not registered in the DI container");
				if (!subtaskService.IsTitleUnique((string)value))
				{
					return new ValidationResult(ErrorMessage ?? "Esta tarea ya se encuentra registrada.");
				}
			}

			return ValidationResult.Success;
		}
    }
}
