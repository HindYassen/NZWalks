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

        public async Task<List<Walk>> GetAll(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            //Select all walks from the database and include the related Difficulty and Region entities
            //return await
            //_nZWalksDbContext.Walks
            //.Include("Difficulty")
            //.Include("Region")
            //.ToListAsync();

            //Select all walks from the database and include the related Difficulty and Region entities using lambda expressions
            //return await
            //_nZWalksDbContext.Walks.Include(w => w.Difficulty).Include(w => w.Region).ToListAsync();

            //using filterOn and filterQuery to filter the walks based on the specified property and value

            var walks = _nZWalksDbContext.Walks.Include(w => w.Difficulty).Include(w => w.Region).AsQueryable();
            //filterOn = "Name" and filterQuery = "Walk1"
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }
            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("LengthInKM", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKM) : walks.OrderByDescending(x => x.LengthInKM);
                }
            }
            //pagination
            var skipResults = (pageNumber - 1) * pageSize;
            walks = walks.Skip(skipResults).Take(pageSize);

            return await walks.ToListAsync();

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
