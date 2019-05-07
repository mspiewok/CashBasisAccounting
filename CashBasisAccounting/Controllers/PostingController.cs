using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CashBasisAccounting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CashBasisAccounting.Data;

namespace CashBasisAccounting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostingController : ControllerBase
    {
        private readonly CashBasisAccountingContext context;
        private readonly ILogger logger;

        public PostingController(CashBasisAccountingContext context, ILogger<PostingController> logger)
        {
            this.logger = logger;
            this.context = context;
        }

        // GET: api/Posting
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Posting>>> GetPosting()
        {
            this.logger.LogInformation("GetPosting [all] started.");
            return await this.context.Postings.ToListAsync();
        }

        // GET: api/Posting/5
        [HttpGet("{id}", Name = "GetPosting")]
        public async Task<ActionResult<Posting>> GetPosting(int id)
        {
            this.logger.LogInformation($"GetPosting started with id: {id}...");
            var posting = await this.context.Postings.FindAsync(id);
            if (posting == null)
            {
                this.logger.LogWarning($"Posting with id {id} not found.");
                return NotFound();
            }

            this.logger.LogInformation("GetPosting successfully executed.");
            return posting;
        }

        // POST: api/Posting
        [HttpPost]
        public async Task<ActionResult<Posting>> PostPosting(Posting posting)
        {
            this.logger.LogInformation($"PostPosting started with id: {posting.Id}...");
            var existingPosting = await this.context.Postings.FindAsync(posting.Id);

            if (existingPosting != null)
            {
                this.logger.LogWarning($"Post called with existing id: {posting.Id}.");
                return BadRequest($"Posting with id {posting.Id} already exists.");
            }

            this.context.Postings.Add(posting);
            await this.context.SaveChangesAsync();

            var postingInDb = (await this.GetPosting(posting.Id)).Value;

            logger.LogInformation("PostPosting successfully executed.");
            return CreatedAtAction(nameof(GetPosting), new { id = posting.Id }, posting);
        }

        // PUT: api/Postings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosting(Posting posting)
        {
            this.logger.LogInformation($"PutPosting started with id: {posting.Id}...");
            var existingPosting = await this.context.Postings.FindAsync(posting.Id);

            if (existingPosting == null)
            {
                this.logger.LogWarning($"Put called with non existing id: {posting.Id}.");
                return BadRequest($"Posting with id {posting.Id} not found.");
            }

            this.context.Entry(existingPosting).CurrentValues.SetValues(posting);
            await this.context.SaveChangesAsync();

            logger.LogInformation("PutPosting successfully executed.");
            return NoContent();
        }

        // DELETE: api/Postings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosting(int id)
        {
            this.logger.LogInformation($"DeletePosting started with id: {id}...");
            var existingPosting = await this.context.Postings.FindAsync(id);

            if (existingPosting == null)
            {
                this.logger.LogWarning($"Delete called with non existing id: {id}.");
                return NotFound($"Posting with id {id} not found.");
            }

            this.context.Postings.Remove(existingPosting);
            await this.context.SaveChangesAsync();

            logger.LogInformation("DeletePosting successfully executed.");
            return NoContent();
        }
    }
}
