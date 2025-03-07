﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Threading.Tasks;

namespace NZWalks.API.Controllers
{
   // api/walks
   [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
      private readonly IMapper mapper;
      private readonly IWalkRepository walkRepository;

      public WalksController(IMapper mapper, IWalkRepository walkRepository)
      {
         this.mapper = mapper;
         this.walkRepository = walkRepository;
      }
      // CREATE Walk
      // POST: /api/Walks
      [HttpPost]
      public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
      {
         // Map DTO to domain model
         var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

         await walkRepository.CreateAsync(walkDomainModel);

         // Map Domain model to DTO

         return Ok(mapper.Map<WalkDto>(walkDomainModel));
      }

      // GET Walks
      // GET: /api/walks
      [HttpGet]
      public async  Task<IActionResult> GetAll(){
         var walksDomainModel = await walkRepository.GetAllAsync();

         //Map Domain Model to DTO
         return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
      }
   }
}
