using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
   // https://localhost:1234/api/regions
   [Route("api/[controller]")]
   [ApiController]
   public class RegionsController : ControllerBase
   {
      private readonly NZWalksDbContext dbContext;
      private readonly IRegionRepository regionRepository;

      public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
      {
         this.dbContext = dbContext;
         this.regionRepository = regionRepository;
      }

      [HttpGet]
      public async Task<IActionResult> GetAll()
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
          */

         // Get Data From Database - Domain models
         var regionsDomain = await regionRepository.GetAllAsync();//await dbContext.Regions.ToListAsync();

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
      public async Task<IActionResult> GetById([FromRoute] Guid id)
      {
         //var region = dbContext.Regions.Find(id);
         // Get Region Domain Model From Database
         var regionDomain = await regionRepository.GetByIdAsync(id);//await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        
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
      public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
      {
         // Map or convert DTO to Domain Model
         var regionDomainModel = new Region
         {
            Code = addRegionRequestDto.Code,
            Name = addRegionRequestDto.Name,
            RegionImageUrl = addRegionRequestDto.RegionImageUrl
         };
         // Use Domain Model to create Region
         regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
         /*await dbContext.Regions.AddAsync(regionDomainModel);
         await dbContext.SaveChangesAsync();*/

         //Map Domain Model back to DTO
         var regionDto = new RegionDto
         {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
         };

         return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
      }

      // Update region
      //PUT: https://localhost:portnumber/api/regions/{id}
      [HttpPut]
      [Route("{id:Guid}")]
      public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
      {
         var regionDomainModel = new Region
         {
            Id = id,
            Code = updateRegionRequestDto.Code,
            Name = updateRegionRequestDto.Name,
            RegionImageUrl = updateRegionRequestDto.RegionImageUrl
         };
         // Check if region exists
         regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
         // var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
         if (regionDomainModel == null)
         {
            return NotFound();
         }
         // Map DTO to Domain Model
         //regionDomainModel.Code = updateRegionRequestDto.Code;
         //regionDomainModel.Name = updateRegionRequestDto.Name;
         //regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
         // Save Changes
         //await dbContext.SaveChangesAsync();

         // Map/Convert Domain Model to DTO
         var regionDto = new RegionDto
         {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
         };
         return Ok(regionDto);
      }
      // Delete Region
      // DELETE: https://localhost:portnumber/api/regions/{id}
      [HttpDelete]
      [Route("{id:Guid}")]
      public async Task<IActionResult> Delete([FromRoute] Guid id)
      {
         //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
         var regionDomainModel = await regionRepository.DeleteAsync(id);
         if (regionDomainModel == null)
         {
            return NotFound();
         }
         // Remove Region
         //dbContext.Regions.Remove(regionDomainModel);
         //await dbContext.SaveChangesAsync();

         // return deleted Region back
         // map Domain Model to DTO
         var regionDto = new RegionDto
         {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageUrl
         };
         return Ok(regionDto);
         //return NoContent();
      }
   }
}
