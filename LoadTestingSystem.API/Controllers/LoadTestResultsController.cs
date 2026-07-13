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
        public IActionResult GetAll()
        {
            var results = _context.LoadTestResults.ToList();
            return Ok(results);
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
                TestDate = dto.TestDate
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
            result.TestDate = dto.TestDate;

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
