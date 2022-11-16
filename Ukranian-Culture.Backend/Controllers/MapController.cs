﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ukranian_Culture.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
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
    }
}
