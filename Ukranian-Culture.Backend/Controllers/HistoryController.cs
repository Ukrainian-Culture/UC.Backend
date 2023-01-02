using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ukranian_Culture.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public HistoryController(IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        [HttpGet("{region:int}")]
        public IActionResult GetHistoryByRegion(string region)
        {
            return Ok();
        }

    }
}
