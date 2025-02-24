using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
         private readonly NZWalksDbContext dbContext;
         public RegionsController(NZWalksDbContext dbContext)
         {
            this.dbContext = dbContext;
         }

         [HttpGet]
         public IActionResult GetAll()
         {
         /*var regions = new List<Region>
            {
               // GET ALL REGIONS
               // GET: https: //localhost/portnumber/api/regions
               new Region
               {
                  Id = Guid.NewGuid(),
                  Name = "Auckland Region",
                  Code = "AKL",
                  RegionImageUrl = "https://www.pexels.com/photo/new-zealand-dockyard-18915023/"
               },
                new Region
               {
                  Id = Guid.NewGuid(),
                  Name = "Wellington Region",
                  Code = "WLG",
                  RegionImageUrl = "https://www.pexels.com/photo/red-rocks-new-zealand-17831396/"
               }
            };


         //INSERT INTO  Regions
         //([Id], [Code], [Name], [RegionImageUrl])
         //values ('d164ca1d-deb7-4a77-86c5-fb9a7148c835', 'AKL', 'Auckland', 'image.jpg')
          */

         // Get Data From Database - Domain models
         var regionsDomain = dbContext.Regions.ToList();

         // Map Domain Models to DTOs
         var regionDto = new List<RegionDto>();
         foreach (var regionDomain in regionsDomain)
         {
            regionDto.Add(new RegionDto
            {
               Id = regionDomain.Id,
               Code = regionDomain.Code,
               Name = regionDomain.Name,
               RegionImageUrl = regionDomain.RegionImageUrl
            });
         }
         // Return DTO
         return Ok(regionDto);

         }
         // GET SINGLE REGION (Get Region By ID)
         // GET: https: //localhost/portnumber/api/regions/{id}
         [HttpGet]
         [Route("{id:Guid}")]
         public IActionResult GetRegion([FromRoute] Guid id)
         {
         //var region = dbContext.Regions.Find(id);
         // Get Region Domain Model From Database
         var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);
         // var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
         
         if (regionDomain == null)
            {
               return NotFound();
            }
         // Map/Conver Region Domain Model to Region DTO
         var regionDto = new RegionDto
         {
            Id = regionDomain.Id,
            Code = regionDomain.Code,
            Name = regionDomain.Name,
            RegionImageUrl = regionDomain.RegionImageUrl
         };
         return Ok(regionDto);
         }

      // POST To Create New Region
      // POST: https://localhost:portnumber/api/regions
      [HttpPost]
      public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
      {
         // Map or convert DTO to Domain Model
         var regionDomainModel = new Region
         {
            Code = addRegionRequestDto.Code,
            Name = addRegionRequestDto.Name,
            RegionImageUrl = addRegionRequestDto.RegionImageUrl
         };
         // Use Domain Model to create Region
         dbContext.Regions.Add(regionDomainModel);
         dbContext.SaveChanges();

         //Map Domain Model back to DTO
         var regionDto = new RegionDto
         {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
         };

         return CreatedAtAction(nameof(GetRegion), new { id = regionDto.Id }, regionDto);
      }
   }
}
