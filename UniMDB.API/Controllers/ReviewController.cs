using Microsoft.AspNetCore.Mvc;
using UniMDB.Application.Dtos;
using UniMDB.Application.Services;
using UniMDB.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace UniMDB.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }


    // CREATE
    [HttpPost("add"), Produces("application/json")]
    public async Task<ActionResult<ReviewResponse>> AddReview([FromBody]ReviewCreation review)
    {
        try
        {
            ReviewResponse reviewNova = await _reviewService.AddReview(review);

            if (reviewNova.Id == 0)
            {
                return BadRequest(review);
            }

            return CreatedAtAction(nameof(GetReview), new { id = reviewNova.Id }, reviewNova);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // READ
    [HttpGet("get/{id}"), Produces("application/json")]
    public async Task<ActionResult<ReviewResponse>> GetReview([FromRoute]uint id)
    {
        try
        {
            ReviewResponse review = await _reviewService.GetReviewById(id);

            if (review.Id == 0)
            {
                review.Id = id;
                return NotFound(review);
            }

            return Ok(review);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("getall/{id_movie}"), Produces("application/json")]
    public async Task<ActionResult<List<ReviewResponseList>>> GetAllReviewsByMovieId([FromRoute] string id_movie)
    {
    try
    {
        var reviews = await _reviewService.GetAllReviewsByMovieId(id_movie);

        if (reviews == null || reviews.Reviews == null || reviews.Reviews.Count == 0)
        {
            return NotFound($"Nenhuma review encontrada para o filme ID ({id_movie}).");
        }

        return Ok(reviews);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
    }

    /*
    [HttpGet("getall/{id_user}"), Produces("application/json")]
    public async Task<ActionResult<List<ReviewResponseAPI>>> GetAllReviewsByUser(uint id_user)
    {
        try
        {
            var reviews = await _reviewService.GetAllReviewsByUserId(id_user);

            if (reviews == null)
            {
                return BadRequest($"Não foi possível delete o ID ( {id_user} )");
            }

            return Ok(reviews);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }
    */
    

    // UPDATE
    [HttpPut("update/{id}"), Produces("application/json")]
    public async Task<ActionResult<ReviewResponse>> UpdateReview([FromQuery]uint id, [FromBody] ReviewUpdate reviewNova)
    {
        try
        {
            ReviewResponse reviewAtualizada = await _reviewService.UpdateReview(id, reviewNova);

            if (reviewAtualizada.Id == 0)
            {
                return BadRequest(reviewNova);
            }

            return Ok(reviewAtualizada);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // DELETE
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<bool>> UpdateReview(uint id)
    {
        try
        {
            bool reviewDeletada = await _reviewService.DeleteReview(id);

            if (reviewDeletada)
            {
                return Ok(reviewDeletada);
            }

            return BadRequest(reviewDeletada);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}