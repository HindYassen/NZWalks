using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repository
{

    public class InMemoryRegionRepository : IRegionRepository
    {
        public Task<IActionResult> Create(AddRegionRequestDto addRegionRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<Region> Create(Region region)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return new List<Region>()
            {

                new Region()
                {
                Id = Guid.NewGuid(),
                Code = "WGN",
                Name = "Wellington",
                RegionImageURL = "https://images.unsplash.com/photo-1507525428034-b723cf961d3e"
            }
            };
        }

        public Task<IActionResult> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Update(Guid id, UpdateRegionRequestDto updateRegionRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<Region?> Update(Guid id, Region region)
        {
            throw new NotImplementedException();
        }

        Task<Region?> IRegionRepository.Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<Region?> IRegionRepository.GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
