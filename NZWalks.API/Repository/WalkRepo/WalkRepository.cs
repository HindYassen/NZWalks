using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System.Drawing;

namespace NZWalks.API.Repository.WalkRepo
{
    public class WalkRepository : IWalkRepository
    {
        public readonly NZWalksDbContext _nZWalksDbContext;
        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<Walk> Create(Walk walk)
        {
            await _nZWalksDbContext.Walks.AddAsync(walk);
            await _nZWalksDbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<List<Walk>> GetAll()
        {
            return await
                //_nZWalksDbContext.Walks
                //.Include("Difficulty")
                //.Include("Region")
                //.ToListAsync();
                _nZWalksDbContext.Walks.Include(w => w.Difficulty).Include(w => w.Region).ToListAsync();
        }

        public async Task<Walk?> GetById(Guid Id)
        {
            return await _nZWalksDbContext.Walks.Include(w => w.Difficulty).Include(w => w.Region).FirstOrDefaultAsync(w => w.Id == Id);
        }

        public async Task<Walk?> Update(Guid Id, Walk walk)
        {
            var existingWalk = await _nZWalksDbContext.Walks.Include(w => w.Difficulty).Include(w => w.Region).FirstOrDefaultAsync(x => x.Id == Id);
            if (existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKM = walk.LengthInKM;
            existingWalk.WalkImageURL = walk.WalkImageURL;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.DifficultyId = walk.DifficultyId;

            existingWalk.Region = _nZWalksDbContext.Regions.FirstOrDefault(r => r.Id == walk.RegionId)!;
            existingWalk.Difficulty = _nZWalksDbContext.Difficulties.FirstOrDefault(d => d.Id == walk.DifficultyId)!;
            await _nZWalksDbContext.SaveChangesAsync();
            return existingWalk;
        }
        public async Task<Walk?> Delete(Guid Id)
        {
            var existingWalk = await _nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);
            if (existingWalk == null)
            {
                return null;
            }
            _nZWalksDbContext.Walks.Remove(existingWalk);
            await _nZWalksDbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
