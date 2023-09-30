using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        //Endpoint: [GET] api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from DB - Domain Models.
            var regionsDomain = await _regionRepository.GetAllAsync();

            //Map Domain models to DTOs.
            var regionsDto = _mapper.Map<List<RegionDTO>>(regionsDomain);

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
            var regionDto = _mapper.Map<RegionDTO>(region);
            
            // Return DTO
            return Ok(regionDto);
        }

        //Endpoint: [POST] /api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDto)
        {
            //map or create from request.
            var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

            //Update DB.
            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

            //Use DTO
            var regionDto = _mapper.Map<RegionDTO>(regionDomainModel);

            //return DTO
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //Endpoint:: [PUT] /api/regions/id
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,
            [FromBody] UpdateRegionRequestDTO updateRegionRequestDto)
        {
            //map to domain model
            var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);

            var getRegion = await _regionRepository.UpdateAsync(id, regionDomainModel);

            if (getRegion == null)
                return NotFound();

            //Map to DTO.
            var regionDto = _mapper.Map<RegionDTO>(getRegion);

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
            var regionDto = _mapper.Map<RegionDTO>(getRegion);

            //Return DTO
            return Ok(regionDto);
        }
    }
}