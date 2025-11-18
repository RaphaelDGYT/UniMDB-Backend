using Bogus.Bson;
using Microsoft.AspNetCore.Mvc;
using UniMDB.Application.Dtos;
using UniMDB.Application.Services;
using UniMDB.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc;
using UniMDB.Infrastructure.Repositories;

namespace UniMDB.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    // utilização do service para meio de comunicação com o repositorys e fazer validações.
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // utilização de [FromBody], [FromQuery] e [FromRoute]
    //[FromQuery] Serve para ler dados que vêm da query string da URL a parte da que vem depois
    //  do ? e separada por &.
    // [FromRoute]Serve para ler dados que vêm da rota da URL
    // Os valores que fazem parte da URL em si, não da query string.
    // [FromBody]Normalmente usado em requisições POST, PUT ou PATCH, quando você envia um objeto JSON ou XML.
    // Os dados não vêm da URL, mas sim do body da requisição.



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
    
    // READ
    [HttpGet("get/{id}"), Produces("application/json")]
    public async Task<ActionResult<UserResponse>> GetUser([FromQuery]uint id)
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
    public async Task<ActionResult<UserReviewsResponse>> GetUserReviews([FromQuery]uint id)
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

    [HttpPost("login"), Produces("application/json")]
    public async Task<ActionResult<LoginResponse>> GetUserBySession([FromBody]UserLogin user)
    {
        try
        {
            LoginResponse userNovo = await _userService.GetUserBySession(user);

            if (userNovo == null)
            {
                    return Unauthorized("Email ou senha inválidos");
            }

            return Ok(userNovo);
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
    [HttpPut("update/{id}"), Produces("application/json")]
    public async Task<ActionResult<UserResponse>> UpdateUser([FromQuery]uint id,[FromBody]UserCreation userNovo)
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