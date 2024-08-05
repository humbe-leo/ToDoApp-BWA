using AutoMapper;
using ToDoApp.Api.DTOs;
using ToDoApp.Api.Models;

namespace ToDoApp.Api.Mapping
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
			CreateMap<Goal, GoalDto>()
				.ForMember(dest => dest.CompletedTasks, opt => opt.MapFrom(src => src.SubTasks.Count(t => t.Status)));
			CreateMap<GoalDto, Goal>();
            CreateMap<SubTask, SubTaskDto>()
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ? "Completada" : "Abierta"));
			CreateMap<SubTaskDto, SubTask>()
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status == "Completada"));
		}
    }
}
