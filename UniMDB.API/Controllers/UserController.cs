using Microsoft.AspNetCore.Mvc;
using UniMDB.Application.Dtos;
using UniMDB.Application.Services;
using UniMDB.Domain.Entities;
using UniMDB.Domain.Interfaces;
namespace UniMDB.API.Controllers;

/*
 
    As classes Controllers não vão ser responsáveis pela implementação das funções que envolvam o banco de dados
    diretamente, aqui somente vamos implementar na API. Essa implementação com o banco de dados será feita no
    Applications.Services

*/

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpGet("getall")]
    public async Task<ActionResult<List<UserResponseAPI>>> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsers();

            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<UserResponseAPI>> GetUser(uint id)
    {

        try
        {

            var user = await _userService.GetUser(id);

            if (user == null)
            {
                return NotFound($"ID {id} não foi encontrado");
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }

    [HttpGet("getbyreview/{id}")]
    public async Task<ActionResult<User>> GetUserByReview(uint id_review)
    {

        try
        { 
            var user = await _userService.GetUserByReview(id_review);

            if (user == null)
            {
                return NotFound($"Usuário não foi encontrado");
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }

    [HttpPost("add")]
    public async Task<ActionResult<UserResponseAPI>> AddUser(UserRegistration user)
    {
        try
        {
            var userNovo = await _userService.AddUser(user);

            if (userNovo.GetType() == typeof(UserResponseAPI))
            {
                return CreatedAtAction(nameof(GetUser), new {id = userNovo.Id}, userNovo);
            }

            return BadRequest(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<UserResponseAPI>> UpdateUser(uint id, UserRegistration userNovo)
    {

        try
        {
            var userAtualizado = await _userService.UpdateUser(id, userNovo);

            if (userAtualizado.GetType() == typeof(UserResponseAPI))
            {
                return Ok(userAtualizado);
            }

            return BadRequest(userNovo);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<User>> DeleteUser(uint id)
    {

        try
        {
            var userDeletado = await _userService.DeleteUser(id);

            if (userDeletado.GetType() == typeof(User))
            {
                return Ok(userDeletado);
            }

            return BadRequest($"Não foi possível delete o ID ( {id} )");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }
}