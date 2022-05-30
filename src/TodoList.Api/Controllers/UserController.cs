using System.Collections.Generic;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> Get()
        {
            var users = await _userService.GetAllAsync();
            var userReadDtos = _mapper.Map<IEnumerable<UserReadDto>>(users);
            return Ok(userReadDtos);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<UserReadDto>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userReadDto = _mapper.Map<UserReadDto>(user);
            return Ok(userReadDto);
        }

        [HttpPost]
        public async Task<ActionResult<UserReadDto>> Create(UserCreateDto userCreateDto)
        {
            var validator = new UserCreateDtoValidator();
            var validatorResult = await validator.ValidateAsync(userCreateDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            var user = _mapper.Map<User>(userCreateDto);

            var createdUser = await _userService.CreateAsync(user);

            var newUser = await _userService.GetByIdAsync(createdUser.Id);

            var userReadDto = _mapper.Map<UserReadDto>(newUser);

            return CreatedAtRoute(nameof(GetById), new { id = userReadDto.Id }, userReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UserCreateDto userCreateDto)
        {
            var validator = new UserCreateDtoValidator();
            var validatorResult = await validator.ValidateAsync(userCreateDto);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var updatedUser = _mapper.Map<User>(userCreateDto);

            await _userService.UpdateAsync(user, updatedUser);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteAsync(user);
            return NoContent();
        }
    }
}