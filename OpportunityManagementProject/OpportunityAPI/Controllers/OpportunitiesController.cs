using Microsoft.AspNetCore.Mvc;
using OpportunitiesBL.Interfaces;
using OpportunityAPI.DTOs;
using OpportunityEF.Models;
using Microsoft.AspNetCore.Authorization;

namespace OpportunityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpportunitiesController : ControllerBase
    {
        private readonly IOpportunityService _service;

        public OpportunitiesController(IOpportunityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
    [FromQuery] OpportunityFilterDTO filter)
        {
            var opportunities =
                await _service.GetFilteredAsync(
                    filter.Keyword,
                    filter.Type,
                    filter.Country,
                    filter.IsFullyFunded,
                    filter.PageNumber,
                    filter.PageSize);

            int totalRecords =
                await _service.CountAsync(
                    filter.Keyword,
                    filter.Type,
                    filter.Country,
                    filter.IsFullyFunded);

            var result =
                new PagedResultDTO<OpportunityDTO>
                {
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize,
                    TotalRecords = totalRecords,
                    TotalPages =
                        (int)Math.Ceiling(
                            totalRecords /
                            (double)filter.PageSize),

                    Data = opportunities.Select(o =>
                        new OpportunityDTO
                        {
                            Id = o.Id,
                            Title = o.Title,
                            Description = o.Description,
                            Type = o.Type,
                            Country = o.Country,
                            Deadline = o.Deadline,
                            IsFullyFunded = o.IsFullyFunded,
                            CreatedAt = o.CreatedAt
                        }).ToList()
                };

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var opportunity = await _service.GetByIdAsync(id);

            if (opportunity == null)
            {

                return NotFound();
            }
             

            return Ok(opportunity);
        }


        [Authorize]

        [HttpPost]
        public async Task<IActionResult> Add(AddOpportunityDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.Deadline <= DateTime.UtcNow)
                return BadRequest("Deadline must be in the future.");

            Opportunity opportunity = new Opportunity
            {
                Title = dto.Title,
                Description = dto.Description,
                Type = dto.Type,
                Country = dto.Country,
                Deadline = dto.Deadline,
                IsFullyFunded = dto.IsFullyFunded,
                CreatedAt = DateTime.UtcNow
            };

            var createdOpportunity = await _service.AddAsync(opportunity);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdOpportunity.Id },
                createdOpportunity);
        }


        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateOpportunityDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.Id != dto.Id)
                return BadRequest("Route Id does not match DTO Id.");

            if (dto.Deadline <= DateTime.UtcNow)
                return BadRequest("Deadline must be in the future.");

            var existingOpportunity =
                await _service.GetByIdAsync(dto.Id);

            if (existingOpportunity == null)
                return NotFound();

            existingOpportunity.Title = dto.Title;
            existingOpportunity.Description = dto.Description;
            existingOpportunity.Type = dto.Type;
            existingOpportunity.Country = dto.Country;
            existingOpportunity.Deadline = dto.Deadline;
            existingOpportunity.IsFullyFunded = dto.IsFullyFunded;

            bool result =
                await _service.UpdateAsync(existingOpportunity);

            if (!result)
                return NotFound();

            return NoContent();
        }


        [Authorize]

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _service.DeleteAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }

    }
}
