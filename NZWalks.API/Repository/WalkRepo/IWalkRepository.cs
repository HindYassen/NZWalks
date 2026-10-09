using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository.WalkRepo
{
    public interface IWalkRepository
    {
        Task<Walk> Create(Walk walk);
        Task<List<Walk>> GetAll();
        Task<Walk?> GetById(Guid Id);
        Task<Walk?> Update(Guid Id, Walk walk);
        Task<Walk?> Delete(Guid Id);
    }
}
