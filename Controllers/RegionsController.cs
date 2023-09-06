using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Endpoint: [GET] api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            //Get Data from DB - Domain Models.
            var regionsDomain = _dbContext.Regions.ToList();

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
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //Get Data from DB - Domain Models.
            var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

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
        public IActionResult Create([FromBody] AddRegionRequestDTO addRegionRequestDto)
        {
            //map or create from request.
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            //Update DB.
            _dbContext.Regions.Add(regionDomainModel);
            _dbContext.SaveChanges();
            
            //Use DTO
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            
            //return DTO
            return CreatedAtAction(nameof(GetById), new {id = regionDomainModel.Id}, regionDto);
        }

        //Endpoint:: [PUT] /api/regions/id
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDto)
        {
            var regionDomainModel = _dbContext.Regions.FirstOrDefault( x=> x.Id == id);

            if (regionDomainModel == null)
                return NotFound();
            
            //Map to domain model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            _dbContext.SaveChanges();
            
            //Map to DTO.
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            
            //Return DTO
            return Ok(regionDto);
        }

        //Endpoint: [DELETE] /api/regions/id
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModel = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
                return NotFound();
            
            //Map DTO.
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            
            //Delete Region
            _dbContext.Regions.Remove(regionDomainModel);
            _dbContext.SaveChanges();
            
            //Return DTO
            return Ok(regionDto);
        }
    }
}