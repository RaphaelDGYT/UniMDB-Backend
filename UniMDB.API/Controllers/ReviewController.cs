using Microsoft.AspNetCore.Mvc;
using UniMDB.Application.Dtos;
using UniMDB.Application.Services;
using UniMDB.Domain.Entities;

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


    [HttpGet("get/{id}")]
    public async Task<ActionResult<ReviewResponseAPI>> GetReviewById(uint id)
    {

        try
        {
            var review = await _reviewService.GetReviewById(id);

            if (review == null)
            {
                return NotFound($"ID {id} não foi encontrado");
            }

            return Ok(review);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
        
    }

    [HttpGet("getall/{id_user}")]
    public async Task<ActionResult<List<ReviewResponseAPI>>> GetAllReviewsByUser(uint id_user)
    {
        try
        {
            var reviews = await _reviewService.GetAllReviewsByUser(id_user);

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

    [HttpPost("add")]
    public async Task<ActionResult<ReviewCreation>> AddReview(ReviewCreation review)
    {
        try
        {
            var reviewNova = await _reviewService.AddReview(review);

            if (reviewNova == null)
            {
                return BadRequest(review);
            }

            return CreatedAtAction("Review criada com sucesso", review);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<ReviewResponseAPI>> UpdateReview(uint id, ReviewCreation review)
    {
        try
        {
            var reviewAtualizada = await _reviewService.UpdateReview(id, review);

            if (reviewAtualizada == null)
            {
                return BadRequest(review);
            }

            return Ok(reviewAtualizada);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<Review>> DeleteUser(uint id)
    {

        try
        {
            var reviewDeletada = await _reviewService.DeleteReview(id);

            if (!reviewDeletada)
            {
                return BadRequest($"Não foi possível delete o ID ( {id} )");
            }

            return Ok(reviewDeletada);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }
}