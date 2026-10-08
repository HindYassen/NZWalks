using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    //https://localhost:7290/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        //Injecting DBContext to Controller with no Abstraction layer no the best practice
        //private readonly NZWalksDbContext _nZWalksDbContext;
        //public RegionsController(NZWalksDbContext nZWalksDbContext )
        //{
        //    _regionRepository = regionRepository;
        //}
        private readonly IRegionRepository _regionRepository;
        //Inject IMapper
        private readonly IMapper _mapper;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }
        //GET:https://localhost:7290/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var regions = new List<Region>
            //{
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Code = "WGN",
            //        Name = "Wellington",
            //        RegionImageURL = "https://images.unsplash.com/photo-1507525428034-b723cf961d3e"
            //    },
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Code = "AUK",
            //        Name = "Auckland",
            //        RegionImageURL = "https://images.unsplash.com/photo-1507525428034-b723cf961d3e"
            //    }
            //};

            //synchronous code
            // var regions = _nZWalksDbContext.Regions.ToList();

            //async code
            //var regions = await _nZWalksDbContext.Regions.ToListAsync();

            //using Abstraction layer insted of direct DBContext in Controller
            var regionsDomain = await _regionRepository.GetAllAsync();


            //Map Domain Model to DTO
            //var regionsDto = new List<RegionDto>();
            //foreach (var region in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageURL = region.RegionImageURL
            //    });
            //}

            //Map Domain Model to DTO using AutoMapper
            var regionsDto = _mapper.Map<List<RegionDto>>(regionsDomain);
            return Ok(regionsDto);

        }
        [HttpGet]
        [Route("{id:Guid}")]
        //GET:https://localhost:7290/api/regions/{id}
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //sync
            //var region = _nZWalksDbContext.Regions.FirstOrDefault(x => x.Id == id);

            //async
            //var region = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            //using Abstraction layer insted of direct DBContext in Controller
            var regionDomain = await _regionRepository.GetById(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map Domain Model to DTO 
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageURL = regionDomain.RegionImageURL
            //};

            //Map Domain Model to DTO using AutoMapper
            var regionDto = _mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }

        //POST:https://localhost:7290/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //MAP or Convert AddRegionRequestDto To Domain Model
            //var regionDomainModel = new Region
            //{
            //    Code = addRegionRequestDto.Code,
            //    Name = addRegionRequestDto.Name,
            //    RegionImageURL = addRegionRequestDto.RegionImageURL
            //};

            //MAP or Convert AddRegionRequestDto To Domain Model using AutoMapper
            var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

            // sync
            // _nZWalksDbContext.Regions.Add(regionDomainModel);
            //_nZWalksDbContext.SaveChanges();

            //async
            //await _nZWalksDbContext.Regions.AddAsync(regionDomainModel);
            //await _nZWalksDbContext.SaveChangesAsync();

            //using Repository layer insted of direct DBContext in Controller_
            regionDomainModel = await _regionRepository.Create(regionDomainModel);

            //MAP or Convert Domain Model To DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageURL = regionDomainModel.RegionImageURL
            //};

            //MAP or Convert Domain Model To DTO using AutoMapper
            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);
            return CreatedAtAction(nameof(Create), new { id = regionDto.Id }, regionDto);
        }

        //PUT https://localhost:7290/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Check if Region Exist
            //sync
            //var regionDomainModel = _nZWalksDbContext.Regions.FirstOrDefault(x => x.Id == id);

            //async
            //var regionDomainModel = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            //using Repository layer insted of direct DBContext in Controller_
            //1.Map updateRegionRequestDto to Domain Model
            //var regionDomainModel = new Region
            //{
            //    Code = updateRegionRequestDto.Code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageURL = updateRegionRequestDto.RegionImageURL
            //};
            //1.Map updateRegionRequestDto to Domain Model using AutoMapper
            var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);

            //2.Update Domain Model in DB using Repository layer
            regionDomainModel = await _regionRepository.Update(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Map DTo to Domain Model
            //regionDomainModel.Code = updateRegionRequestDto.Code;
            //regionDomainModel.Name = updateRegionRequestDto.Name;
            //regionDomainModel.RegionImageURL = updateRegionRequestDto.RegionImageURL;

            //sync
            //_nZWalksDbContext.SaveChanges();

            //async
            //await _nZWalksDbContext.SaveChangesAsync();

            //Map DTo to Domain Model
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageURL = regionDomainModel.RegionImageURL
            //};
            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);

        }

        //DELETE:https://localhost:7290/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //sync
            //var regionDomainModel = _nZWalksDbContext.Regions.FirstOrDefault(x => x.Id == id);

            //async
            //var regionDomainModel = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            //using Repository layer insted of direct DBContext in Controller
            var regionDomainModel = await _regionRepository.Delete(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //async
            //_nZWalksDbContext.Regions.Remove(regionDomainModel);

            // Sync
            // _nZWalksDbContext.SaveChanges();

            //async
            //await _nZWalksDbContext.SaveChangesAsync();

            //Map DTo to Domain Model
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageURL = regionDomainModel.RegionImageURL
            //};
            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }
    }
}
