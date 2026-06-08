using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

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

         
            var regionsDomain = dbContext.Regions.ToList();
            //map Domain Model to DTO
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }
            // Return DTOs to client
            return Ok(regionsDto);
        }
        // GET: Single Region(by id)
        //https://localhost:44328/api/regions/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {

            //var region = dbContext.Regions.Find(id);
            //Get Region Domain Model From Database
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map/convert Region Model to Region Dto
            var RegionDto = new RegionDto
            {

                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            // Return DTO back to client
            return Ok(RegionDto);
        }

        //Post To Create New Region
        //Post :https://localhost:44328/api/regions
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map/Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // Use Domain Model to create Region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            //Map Domain Model Back to DTO 
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl

            };
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //Update region
        //PUT: https://localhost:44328/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)

        {
           //check if region exists
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();

            }
            // Map DTO to Domain Model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            dbContext.SaveChanges();

            //Convert Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);


        }
    }
}
