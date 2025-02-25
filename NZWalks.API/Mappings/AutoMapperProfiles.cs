using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
   public class AutoMapperProfiles : Profile
   {
      public AutoMapperProfiles()
      {
         CreateMap<Region, RegionDto>().ReverseMap();
         CreateMap<AddRegionRequestDto, RegionDto>().ReverseMap();
         CreateMap<UpdateRegionRequestDto, RegionDto>().ReverseMap();


         //CreateMap<UserDTO, UserDomain>()
         //   .ForMember(x => x.Name, opt => opt.MapFrom(x => x.FullName))
         //   .ReverseMap();
      }
   }


   //public class UserDTO
   //{
   //   public string FullName { get; set; }
   //}
   //public class UserDomain
   //{
   //   public string Name { get; set; }
   //}
}
