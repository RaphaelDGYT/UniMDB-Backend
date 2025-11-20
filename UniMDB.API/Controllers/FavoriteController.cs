using Microsoft.AspNetCore.Mvc;
using UniMDB.Application.Dtos;
using UniMDB.Application.Services;
using UniMDB.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace UniMDB.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoriteController : ControllerBase
{
    private readonly IFavoriteService _favoriteService;

    public FavoriteController(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }


    // CREATE
    [HttpPost("add"), Produces("application/json")]
    public async Task<ActionResult<FavoriteResponse>> AddFavorite([FromBody]FavoriteCreation favorite)
    {
        try
        {
            FavoriteResponse favoriteNovo = await _favoriteService.AddFavorite(favorite);

            if (favoriteNovo.Id_Favorite == 0)
            {
                return BadRequest(favorite);
            }

            return CreatedAtAction(nameof(Getfavorite), new { favoriteNovo.Id_Favorite }, favoriteNovo);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // READ
    [HttpGet("get/{id}"), Produces("application/json")]
    public async Task<ActionResult<FavoriteResponse>> Getfavorite([FromRoute]uint id)
    {
        try
        {
            FavoriteResponse favorite = await _favoriteService.GetFavoriteById(id);

            if (favorite.Id_Favorite == 0)
            {
                favorite.Id_Favorite = id;
                return NotFound(favorite);
            }

            return Ok(favorite);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    

    // DELETE
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<bool>> DeleteFavorite(uint id)
    {
        try
        {
            bool favoriteDeletada = await _favoriteService.DeleteFavorite(id);

            if (favoriteDeletada)
            {
                return Ok(favoriteDeletada);
            }

            return BadRequest(favoriteDeletada);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}