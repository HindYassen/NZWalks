using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilter;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.WalksDto;
using NZWalks.API.Repository.WalkRepo;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        public readonly IWalkRepository _walkRepository;
        public readonly IMapper _mapper;
        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }
        //Create Walk
        //Post:https:localhost:7290/api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //use manual validation for addWalkRequestDto because we are not using [ApiController] attribute in this controller
            //if (ModelState.IsValid)
            //{
            //Map addWalkRequestDto to domain model
            var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDto);

                //Pass details to repository
                var createdWalk = await _walkRepository.Create(walkDomainModel);

                //MAP or Convert Domain Model To DTO using AutoMapper
                var walkDto = _mapper.Map<WalkDto>(createdWalk);
                return CreatedAtAction(nameof(Create), new { id = walkDto.Id }, walkDto);
            //}
            //else
            //{
            //    return BadRequest(ModelState);
            //}
        }

        //GET ALL Walks
        //GET:https://localhost7290/api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walks = await _walkRepository.GetAll();
            var walkDtos = _mapper.Map<List<WalkDto>>(walks);
            return Ok(walkDtos);
        }

        //GET Walk By Id
        //GET:https://localhost:7290/api/walks/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await _walkRepository.GetById(id);
            if (walk == null)
            {
                return NotFound();
            }
            var walkDto = _mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        //Updare Walk
        //Put:https://localhost:7290/api/walks/{id}
        [HttpPut]
        [ValidateModel]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            //use manual validation for updateWalkRequestDto because we are not using [ApiController] attribute in this controller
            //if (ModelState.IsValid)
            //{
            //Map updateWalkRequestDto to domain model
            var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);
                //Pass details to repository
                var updatedWalk = await _walkRepository.Update(id, walkDomainModel);
                if (updatedWalk == null)
                {
                    return NotFound();
                }
                //MAP or Convert Domain Model To DTO using AutoMapper
                var walkDto = _mapper.Map<WalkDto>(updatedWalk);
                return Ok(walkDto);
            //}
            //else
            //{
            //    return BadRequest(ModelState);
            //}
        }

        //Delete Walk
        //DELETE:https://localhost:7290/api/walks/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalk = await _walkRepository.Delete(id);
            if (deletedWalk == null)
            {
                return NotFound();
            }
            var walkDto = _mapper.Map<WalkDto>(deletedWalk);
            return Ok(walkDto);
        }
    }
}
