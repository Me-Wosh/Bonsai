using AutoMapper;
using Bonsai.Models;

namespace Bonsai.AutoMapper.Profiles;

public class ToDoProfile : Profile
{
    public ToDoProfile()
    {
        CreateMap<FormToDo, ToDo>().ReverseMap();
    }
}