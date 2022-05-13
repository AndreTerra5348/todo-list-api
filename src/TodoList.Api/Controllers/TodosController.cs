using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Dtos;
using TodoList.Api.Validators;
using TodoList.Core.Models;
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

        [HttpGet("{id}", Name = "GetTodoById")]
        public async Task<ActionResult<TodoReadDto>> GetTodoById(int id)
        {
            var todo = await _todoService.GetByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            var todoReadDto = _mapper.Map<TodoReadDto>(todo);
            return Ok(todoReadDto);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<TodoReadDto>>> GetByUserId(int id)
        {
            var todos = await _todoService.GetByUserIdAsync(id);
            var todoReadDtos = _mapper.Map<IEnumerable<TodoReadDto>>(todos);
            return Ok(todoReadDtos);
        }

        [HttpPost]
        public async Task<ActionResult<TodoReadDto>> Create(TodoCreateDto todoCreateDto)
        {
            var validator = new TodoCreateDtoValidator();
            var validatorResult = await validator.ValidateAsync(todoCreateDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            var todo = _mapper.Map<Todo>(todoCreateDto);
            await _todoService.CreateAsync(todo);

            var newTodo = await _todoService.GetByIdAsync(todo.Id);

            var todoReadDto = _mapper.Map<TodoReadDto>(newTodo);
            return CreatedAtRoute(nameof(GetTodoById), new { id = todoReadDto.Id }, todoReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoReadDto>> Update(int id, TodoCreateDto todoCreateDto)
        {
            var validator = new TodoCreateDtoValidator();
            var validatorResult = await validator.ValidateAsync(todoCreateDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            var todo = await _todoService.GetByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            var updatedTodo = _mapper.Map<Todo>(todoCreateDto);
            await _todoService.UpdateAsync(todo, updatedTodo);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var todo = await _todoService.GetByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            await _todoService.DeleteAsync(todo);
            return NoContent();
        }
    }
}