using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository.WalkRepo
{
    public interface IWalkRepository
    {
        Task<Walk> Create(Walk walk);
        Task<List<Walk>> GetAll(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetById(Guid Id);
        Task<Walk?> Update(Guid Id, Walk walk);
        Task<Walk?> Delete(Guid Id);
    }
}
