using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Dtos;
using TodoList.Core.Services;

namespace TodoList.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;

        public TodosController(ITodoService todoService, IMapper mapper)
        {
            _todoService = todoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoReadDto>>> Get()
        {
            var todos = await _todoService.GetAllWithUserAsync();
            var todoReadDtos = _mapper.Map<IEnumerable<TodoReadDto>>(todos);
            return Ok(todoReadDtos);
        }
    }
}