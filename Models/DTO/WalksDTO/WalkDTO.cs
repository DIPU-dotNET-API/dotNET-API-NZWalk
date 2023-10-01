using NZWalksAPI.Models.DTO.DifficultiesDTO;

namespace NZWalksAPI.Models.DTO.WalksDTO;

public class WalkDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double LengthInKm { get; set; }
    public string? WalkImageUrl { get; set; }
    
    //Navigation Properties
    public DifficultyDTO Difficulty { get; set; }
    public RegionDTO Region { get; set; }
}