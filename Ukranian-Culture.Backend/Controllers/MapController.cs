using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ukranian_Culture.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly RepositoryContext _repositoryContext;

        public MapController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        [HttpGet]
        public IActionResult GetCitys()
        {
            return Ok(new[]
            {
                new {city = "I-F"},
                new {city = "Lviv"},
                new {city = "Kyiv"},
                new {city = "Odessa"}
            });
        }

        [HttpGet("Users")]
        public IActionResult GetUsers()
        {
            return Ok(_repositoryContext.Users.AsNoTracking());
        }
    }
}
