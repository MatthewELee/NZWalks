using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
   public class InMemoryRegionRepository : IRegionRepository
   {
      public async Task<List<Region>> GetAllAsync()
      {
         return new List<Region>
         {
            new Region
            {
               Id = Guid.NewGuid(),
               Name = "In memory Region",
               Code = "IMR",
               RegionImageUrl = "https://www.pexels.com/photo/new-zealand-dockyard-18915023/"
            }
         };
      }
   }
}
