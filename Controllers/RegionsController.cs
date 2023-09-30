using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;

        private readonly IRegionRepository _regionRepository;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
        }

        //Endpoint: [GET] api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from DB - Domain Models.
            var regionsDomain = await _regionRepository.GetAllAsync();

            //Map Domain models to DTOs.
            var regionsDto = new List<RegionDTO>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDTO()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            //Return DTOs.
            return Ok(regionsDto);
        }

        //Endpoint: [GET] api/regions/id
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get Data from DB - Domain Models.
            var region = await _regionRepository.GetByIdAsync(id);

            if (region == null)
                return NotFound();

            //Map Domain models to DTOs.
            var regionDto = new RegionDTO()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            // Return DTO
            return Ok(regionDto);
        }

        //Endpoint: [POST] /api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDto)
        {
            //map or create from request.
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            //Update DB.
            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

            //Use DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            //return DTO
            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }

        //Endpoint:: [PUT] /api/regions/id
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,
            [FromBody] UpdateRegionRequestDTO updateRegionRequestDto)
        {
            //map to domain model
            var regionDomainModel = new Region
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };

            var getRegion = await _regionRepository.UpdateAsync(id, regionDomainModel);

            if (getRegion == null)
                return NotFound();

            //Map to DTO.
            var regionDto = new RegionDTO
            {
                Id = getRegion.Id,
                Code = getRegion.Code,
                Name = getRegion.Name,
                RegionImageUrl = getRegion.RegionImageUrl
            };

            //Return DTO
            return Ok(regionDto);
        }

        //Endpoint: [DELETE] /api/regions/id
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var getRegion = await _regionRepository.DeleteAsync(id);

            if (getRegion == null)
                return NotFound();
            
            //Map to Dto
            var regionDto = new RegionDTO
            {
                Id = getRegion.Id,
                Code = getRegion.Code,
                Name = getRegion.Name,
                RegionImageUrl = getRegion.RegionImageUrl
            };

            //Return DTO
            return Ok(regionDto);
        }
    }
}