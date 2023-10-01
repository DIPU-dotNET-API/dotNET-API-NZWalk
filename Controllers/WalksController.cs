using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO.WalksDTO;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddWalkRequestDTO addWalkRequestDto)
        {
            //Map DTO to Domain Model
            
            
            //Map to DTO
            
            
            //Return DTO
            return Ok();
        }
    }
}