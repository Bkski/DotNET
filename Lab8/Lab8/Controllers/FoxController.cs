using Lab8.Data;
using Lab8.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;


namespace Lab8.Controllers
{
    [ApiController]
    [Route("api/fox")]
    public class FoxController : ControllerBase
    {
        private readonly IFoxesRepository _repo;

        public FoxController(IFoxesRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Fox>> Get()
        {
            var foxes = _repo.GetAll()
                             .OrderByDescending(f => f.Loves)
                             .ThenBy(f => f.Hates);

            return Ok(foxes);
        }

        [HttpGet("{id}")]
        public ActionResult<Fox> Get(int id)
        {
            var fox = _repo.Get(id);
            if (fox == null)
                return NotFound();
            return Ok(fox);
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Fox> Post([FromBody] Fox fox)
        {
            _repo.Add(fox);
            return CreatedAtAction(nameof(Get), new { id = fox.Id }, fox);
        }

        [HttpPut("love/{id}")]
        public IActionResult Love(int id)
        {
            var fox = _repo.Get(id);
            if (fox == null)
                return NotFound();

            fox.Loves++;
            _repo.Update(id, fox);
            return Ok(fox);
        }

        [HttpPut("hate/{id}")]
        public IActionResult Hate(int id)
        {
            var fox = _repo.Get(id);
            if (fox == null)
                return NotFound();

            fox.Hates++;
            _repo.Update(id, fox);
            return Ok(fox);
        }
    }
}
