using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    //https://localhost:44328/api/regions
    /* [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository ;

        public IRegionRepository RegionRepository { get; }

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.RegionRepository = regionRepository;
        } */

    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(
            NZWalksDbContext dbContext,
            IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        // GET: All Regions
        //https://localhost:44328/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
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


            var regionsDomain = await regionRepository.GetAllAsync();
            //map Domain Model to DTO
            /* var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            } */
            //Map Domain model to DTOs
            
            // Return DTOs to client
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }
        // GET: Single Region(by id)
        //https://localhost:44328/api/regions/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {

            //var region = dbContext.Regions.Find(id);
            //Get Region Domain Model From Database
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map/convert Region Model to Region Dto
            
            // Return DTO back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        //Post To Create New Region
        //Post :https://localhost:44328/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map/Convert DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Use Domain Model to create Region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //Map Domain Model Back to DTO 
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //Update region
        //PUT: https://localhost:44328/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)

        {
            // Map DTO to Domain Model
            var regionDomainModel = mapper.Map<RegionDto>(updateRegionRequestDto);
            //check if region exists
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();

            }
            // Map DTO to Domain Model

            //Convert Domain Model to DTO
        
            return Ok(mapper.Map<RegionDto>(regionDomainModel));


        }

        //Delete Region
        //DELETE: https://localhost:44328/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            
            // return deleted Region back
            //map Domain model to DTO
          
            return Ok(mapper.Map<RegionDto>(regionDomainModel));

   }
    }
}
