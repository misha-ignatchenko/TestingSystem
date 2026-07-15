using LoadTestingSystem.API.Data;
using LoadTestingSystem.API.DTOs;
using LoadTestingSystem.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoadTestingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoadTestResultsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoadTestResultsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
                return BadRequest("page і pageSize повинні бути більше 0.");

            var totalCount = _context.LoadTestResults.Count();

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var results = _context.LoadTestResults.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var response = new PagedResult<LoadTestResult>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Items = results
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateLoadTestResult dto)
        {
            var result = new LoadTestResult
            {
                TestName = dto.TestName,
                RequestsPerSecond = dto.RequestsPerSecond,
                AverageResponseTime = dto.AverageResponseTime,
                ErrorCount = dto.ErrorCount,
                TestDate = DateTime.Now
            };

            _context.LoadTestResults.Add(result);

            _context.SaveChanges();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CreateLoadTestResult dto)
        {
            var result = _context.LoadTestResults.FirstOrDefault(r => r.Id == id);

            if (result == null)
                return NotFound();

            result.TestName = dto.TestName;
            result.RequestsPerSecond = dto.RequestsPerSecond;
            result.AverageResponseTime = dto.AverageResponseTime;
            result.ErrorCount = dto.ErrorCount;
            result.TestDate = DateTime.Now;

            _context.SaveChanges();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _context.LoadTestResults.FirstOrDefault(r => r.Id == id);

            if (result == null)
                return NotFound();

            _context.LoadTestResults.Remove(result);

            _context.SaveChanges();

            return Ok();
        }
    }
}
