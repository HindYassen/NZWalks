using NZWalks.API.Models.DTO.DifficultiesDto;

namespace NZWalks.API.Models.DTO.WalksDto
{
    public class WalkDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKM { get; set; }

        public string? WalkImageURL { get; set; }

        public DifficultyDto Difficulty { get; set; }
        
        public RegionDto Region { get; set; }
    }
}
