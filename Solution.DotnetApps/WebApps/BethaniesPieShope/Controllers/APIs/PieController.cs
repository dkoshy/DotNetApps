using BethaniesPieShope.Models;
using BethaniesPieShope.Repos;
using BethaniesPieShope.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace BethaniesPieShope.Controllers.APIs;

[Route("api/Pies")]
[ApiController]
public class PieController : ControllerBase
{
    private readonly IPieRepository _pieRepository;

    public PieController(IPieRepository pieRepository)
    {
        _pieRepository = pieRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PieDto>> Allpies()
    {
        var pies = _pieRepository.AllPies.Select(p => new PieDto
        {
            PieId = p.PieId,
            Price = p.Price,
            Name = p.Name,
            LongDescription = p.LongDescription,
            ShortDescription = p.ShortDescription,
            ImageThumbnailUrl = p.ImageThumbnailUrl,
            ImageUrl = p.ImageUrl,
            CategoryId = p.CategoryId,
            InStock = p.InStock,
            CategoryName = p.Category.CategoryName
        });
        if (pies == null)
        {
            return Ok(new List<PieDto>());
        }

        return Ok(pies);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetPieById([FromRoute] int id)
    {
        var pie = _pieRepository.GetPieById(id);
        if (pie == null)
        {
            return NotFound();
        }
        return Ok(new PieDto
        {
            PieId = pie.PieId,
            Price = pie.Price,
            Name = pie.Name,
            LongDescription = pie.LongDescription,
            ShortDescription = pie.ShortDescription,
            ImageThumbnailUrl = pie.ImageThumbnailUrl,
            ImageUrl = pie.ImageUrl,
            CategoryId = pie.CategoryId,
            InStock = pie.InStock,
            CategoryName = pie.Category.CategoryName
        });
    }

    [HttpGet("search")]
    public IActionResult SearchPies([FromQuery] string term)
    {
        var pies = _pieRepository.SearchPies(term)
                      .Select(p => new PieDto
                      {
                          PieId = p.PieId,
                          Price = p.Price,
                          Name = p.Name,
                          LongDescription = p.LongDescription,
                          ShortDescription = p.ShortDescription,
                          ImageThumbnailUrl = p.ImageThumbnailUrl,
                          ImageUrl = p.ImageUrl,
                          CategoryId = p.CategoryId,
                          InStock = p.InStock,
                          CategoryName = p.Category.CategoryName
                      });
        if (pies == null || !pies.Any())
        {
            return Ok(new List<PieDto>());
        }
        return Ok(pies);
    }

}
