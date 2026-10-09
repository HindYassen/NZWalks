namespace NZWalks.API.Models.DTO.WalksDto
{
    public class UpdateWalkRequestDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKM { get; set; }

        public string? WalkImageURL { get; set; }

        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; }
    }
}
