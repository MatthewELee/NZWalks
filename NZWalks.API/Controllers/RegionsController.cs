using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

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
            var regions = dbContext.Regions.ToList();
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
         
          
     INSERT INTO  Regions
     ([Id], [Code], [Name], [RegionImageUrl])
     values ('d164ca1d-deb7-4a77-86c5-fb9a7148c835', 'AKL', 'Auckland', 'image.jpg')
             */

            return Ok(regions);

         }
         // GET SINGLE REGION (Get Region By ID)
         // GET: https: //localhost/portnumber/api/regions/{id}
         [HttpGet]
         [Route("{id}")]
         public IActionResult GetRegion(Guid id)
         {
         //var region = dbContext.Regions.Find(id);
         var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
         if (region == null)
            {
               return NotFound();
            }
            return Ok(region);
         }
   }
}
