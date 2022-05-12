using AutoMapper;
using TodoList.Api.Dtos;
using TodoList.Core.Models;

namespace TodoList.Api.Profiles
{
    public class TodoListProfile : Profile
    {
        public TodoListProfile()
        {
            CreateMap<Todo, TodoReadDto>();
            CreateMap<TodoCreateDto, Todo>();
        }
    }
}