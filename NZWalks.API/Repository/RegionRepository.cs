using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using System.Drawing.Drawing2D;

namespace NZWalks.API.Repository
{
    public class RegionRepository : IRegionRepository
    {
        public readonly NZWalksDbContext _nZWalksDbContext;
        public RegionRepository(NZWalksDbContext NZWalksDbContext)
        {
            _nZWalksDbContext = NZWalksDbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetById(Guid id)
        {
            return await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id); ;
        }
        public async Task<Region> Create(Region region)
        {

            await _nZWalksDbContext.Regions.AddAsync(region);
            await _nZWalksDbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> Update(Guid id, Region updateRegionRequestDto)
        {
            var existingRegion = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            //Map DTo to Domain Model
            existingRegion.Code = updateRegionRequestDto.Code;
            existingRegion.Name = updateRegionRequestDto.Name;
            existingRegion.RegionImageURL = updateRegionRequestDto.RegionImageURL;
            await _nZWalksDbContext.SaveChangesAsync();

            return existingRegion;
        }
        public async Task<Region?> Delete(Guid id)
        {
            var existingRegion = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            _nZWalksDbContext.Regions.Remove(existingRegion);
            await _nZWalksDbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
