using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    //https://localhost:44328/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: All Regions
        //https://localhost:44328/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            //hard coded data - to be replaced with data from database
            /* var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "AUK",
                    Name = "Auckland",
                    RegionImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Auckland_skyline_from_Mt_Eden.jpg/2560px-Auckland_skyline_from_Mt_Eden.jpg"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "WGN",
                    Name = "Wellington",
                    RegionImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3b/Wellington_City.jpg/2560px-Wellington_City.jpg"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "CAN",
                    Name = "Canterbury",
                    RegionImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d7/Christchurch_Cathedral_Square.jpg/2560px-Christchurch_Cathedral_Square.jpg"
                }
            };
            return Ok(regions);
            */
            var regions = dbContext.Regions.ToList();
            return Ok(regions);
        }
        // GET: Single Region(by id)
        //https://localhost:44328/api/regions/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = dbContext.Regions.Find(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }
    }
}
