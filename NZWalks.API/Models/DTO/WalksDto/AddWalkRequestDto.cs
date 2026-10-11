using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.WalksDto
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name must be at most 100 characters long")]
        public string Name { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Description must be at most 500 characters long")]
        public string Description { get; set; }

        [Required]
        [Range(0,50, ErrorMessage = "LengthInKM must be between 0 and 50")]
        public double LengthInKM { get; set; }

        public string? WalkImageURL { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }

    }
}
