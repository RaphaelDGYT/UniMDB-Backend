using Microsoft.AspNetCore.Mvc;
using UniMDB.Infrastructure.Data;

namespace UniMDB.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController
{
    private readonly AppDbContext _appDbContext;

    public ReviewController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
}