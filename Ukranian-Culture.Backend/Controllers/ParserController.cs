using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ukranian_Culture.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParserController : ControllerBase
{

    private readonly IParser _parser;
    private readonly ILoggerManager _logger;

    public ParserController(IParser parser, ILoggerManager logger)
    {
        _parser = parser;
        _logger = logger;
    }

    [HttpGet("~/GetUkranePopulation")]
    public async Task<IActionResult> GetUkranePopulation()
    {
        var node = await _parser
            .GetNodeByUrl(@"https://index.minfin.com.ua/ua/reference/people/", "//td[@align='right']");

        if (node == null)
        {
            _logger.LogError("Node for Ukraine population is absent");
            return BadRequest();
        }

        var result = node
            .Take(1)
            .Aggregate("", (str, el) => str + el.InnerHtml)
            .Replace("<big>", "")
            .Replace("</big>", "")
            .Replace("&nbsp;", "")
            .Replace(",", "") + "00";

        return Ok(Convert.ToInt32(result));
    }

    [HttpGet("~/GetAmountOfUnescoHeritage")]
    public async Task<IActionResult> GetAmountOfUnescoHeritage()
    {
        var node = await _parser
            .GetNodeByUrl(@"https://vue.gov.ua/%D0%A1%D0%BF%D0%B8%D1%81%D0%BE%D0%BA_%D0%B2%D1%81%D0%B5%D1%81%D0%B2%D1%96%D1%82%D0%BD%D1%8C%D0%BE%D1%97_%D1%81%D0%BF%D0%B0%D0%B4%D1%89%D0%B8%D0%BD%D0%B8_%D0%AE%D0%9D%D0%95%D0%A1%D0%9A%D0%9E", "//div[@class='mw-parser-output']/ol");

        if (node == null)
        {
            _logger.LogError("Node for UNESCO heritage is absent");
            return BadRequest();
        }

        var result = node
            .First()
            .ChildNodes
            .Where((_, i) => i % 2 == 0)
            .Count();

        return Ok(result);
    }

    [HttpGet("~/GetPopulationOfRegions")]
    public async Task<IActionResult> GetPopulationOfRegions()
    {
        var preFormatedNode = await _parser.GetNodeByUrl(@"https://index.minfin.com.ua/ua/reference/people/", "//div[@class='sort2-table']/table/tr/td[@align='right' or @align='left']");

        var node = preFormatedNode
           .Where((_, i) => i % 4 == 0 || i % 4 == 1)
           .Select(str => str.InnerHtml);

        if (node == null)
        {
            _logger.LogError("Node for population by regions is absent");
            return BadRequest();
        }

        var result = node
            .Where((_, i) => i % 2 == 0)
            .Select(elem => elem.Replace("<span class='idx-inline-400'>&nbsp;обл.</span>", ""))
            .ToList()
            .Zip(node
                    .Where((_, i) => i % 2 == 1)
                    .Select(elem => Convert.ToInt32(elem.Replace(",", "") + "00"))
                    .ToList(), (x, y) => new KeyValuePair<string, int>(x, y))
            .OrderByDescending(elem => elem.Value)
            .Take(5);

        return Ok(result);
    }

    [HttpGet("~/GetAmountOfMonuments")]
    public async Task<IActionResult> GetAmountOfMonuments()
    {
        var node = await _parser
            .GetNodeByUrl(@"https://www.ukrinform.ua/rubric-society/3152310-v-ukraini-e-majze-170-tisac-pamatok-i-65-istorikokulturnih-zapovidnikiv-mkip.html", "//h1[@class='newsTitle']");

        if (node == null)
        {
            _logger.LogError("Node for amount of monuments is absent");
            return BadRequest();
        }

        var result = node
            .First()
            .InnerHtml
            .Replace("В Україні є майже", "")
            .Replace(" тисяч пам&#039;яток і 65 історико-культурних заповідників – МКІП", "");

        return Ok(Convert.ToInt32(result + "000"));
    }
}

