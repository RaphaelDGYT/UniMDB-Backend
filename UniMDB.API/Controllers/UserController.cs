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
    public async Task<ActionResult<UserResponse>> AddUser([FromBody]UserCreation user)
    {
        try
        {
            UserResponse userNovo = await _userService.AddUser(user);

            if (userNovo.Id == 0)
            {
                return BadRequest(user);
            }

            return CreatedAtAction(nameof(GetUser), new { userNovo.Id }, userNovo);

        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpPost("login"), Produces("application/json")]
    public async Task<ActionResult<UserResponse>> Login([FromBody]UserLogin user)
    // para evitar erros da ligação do front via json utilize [FromBody] para mudança ou adção ao banco de dados,
    // [FromQuerry] para efetuar buscas.
    {
        try
        {
            UserResponse userNovo = await _userService.Login(user);

            if (userNovo.Id == 0)
            {
                return BadRequest(user);
            }

            return CreatedAtAction(nameof(GetUser), new { userNovo.Id }, userNovo);

        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    // READ
    [HttpGet("get/{id}"), Produces("application/json")]
    public async Task<ActionResult<UserResponse>> GetUser([FromQuerry]uint id)
    {
        try
        {
            UserResponse user = await _userService.GetUserById(id);

            if (user.Id == 0)
            {
                user.Id = id;
                return NotFound(user);
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("getreviews/{id}"), Produces("application/json")]
    public async Task<ActionResult<UserReviewsResponse>> GetUserReviews([FromQuerry]uint id)
    {
        try
        {
            UserReviewsResponse reviews = await _userService.GetAllReviewsByUserId(id);

            if (reviews.UserAutor.id_user == 0)
            {
                NotFound(reviews);
            }

            return Ok(reviews);
        }
        catch
        {
            throw;
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
    [HttpPut("update/{id}"), Produces("application/json")]
    public async Task<ActionResult<UserResponse>> UpdateUser([FromBody]uint id, UserCreation userNovo)
    {
        try
        {
            UserResponse userAtualizado = await _userService.UpdateUser(id, userNovo);

            if (userAtualizado.Id == 0)
            {
                return BadRequest(userNovo);
            }

            return Ok(userAtualizado);
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
            bool userDeletado = await _userService.DeleteUser(id);

            if (userDeletado)
            {
                return Ok(userDeletado);
            }

            return BadRequest(userDeletado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}