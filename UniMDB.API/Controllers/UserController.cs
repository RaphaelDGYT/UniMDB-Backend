using Bogus.Bson;
using Microsoft.AspNetCore.Mvc;
using UniMDB.Application.Dtos;
using UniMDB.Application.Services;
using UniMDB.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace UniMDB.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    // CREATE
    [HttpPost("add"), Produces("application/json")]
    public async Task<ActionResult<UserResponseAPI>> AddUser(UserRegistration user)
    {
        try
        {
            var userNovo = await _userService.AddUser(user);

            if (userNovo != null)
            {
                return CreatedAtAction(nameof(GetUser), new { userNovo.Id }, userNovo);
            }

            return BadRequest(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    // READ
    [HttpGet("get/{id}"), Produces("application/json")]
    public async Task<ActionResult<UserResponseAPI>> GetUser(uint id)
    {
        try
        {
            var user = await _userService.GetUserById(id);

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

    /*
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
    */
    
    // UPDATE
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
    
    // DELETE
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<bool>> DeleteUser(uint id)
    {

        try
        {
            var userDeletado = await _userService.DeleteUser(id);

            if (userDeletado.GetType() == typeof(bool))
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