using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
      [HttpGet]
      public IActionResult GetAll()
      {
         var regions = new List<Region>
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

         return Ok(regions);
      }
    }
}
