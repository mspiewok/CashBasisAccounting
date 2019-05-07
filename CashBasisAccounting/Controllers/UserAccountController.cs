using CashBasisAccounting.Data;
using CashBasisAccounting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CashBasisAccounting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly CashBasisAccountingContext context;
        private readonly IConfiguration config;
        private readonly ILogger logger;
        private readonly IAuthRepository repository;

        public UserAccountController(IAuthRepository repository,
                                     ILogger<UserAccountController> logger,
                                     CashBasisAccountingContext context,
                                     IConfiguration config)
        {
            this.repository = repository;
            this.logger = logger;
            this.context = context;
            this.config = config;
        }

        // GET: api/UserAccount 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAccount>>> GetUserAccount()
        {
            this.logger.LogInformation("GetUserAccount [all] started.");
            return await this.context.UserAccounts.ToListAsync();
        }

        // GET: api/UserAccount/{string}
        [HttpGet("{id}", Name = "GetUserAccount")]
        public async Task<ActionResult<UserAccount>> GetUserAccount(int id)
        {
            this.logger.LogInformation($"GetUserAccount started with id: {id}...");
            var userAccount = await this.context.UserAccounts.FindAsync(id);
            if (userAccount == null)
            {
                this.logger.LogWarning($"UserAccount with id {id} not found.");
                return NotFound();
            }

            this.logger.LogInformation("GetUserAccount successfully executed.");
            return userAccount;
        }

        // POST: api/UserAccounts
        [HttpPost("register")]
        public async Task<ActionResult<UserAccount>> Register(UserAccountDto userAccountDto)
        {
            this.logger.LogInformation($"PostUserAccount started with username: {userAccountDto.Username}...");
            
            if (await repository.UserAccountExists(userAccountDto.Username))
            {
                this.logger.LogWarning($"Post called with existing username: {userAccountDto.Username} found.");
                return BadRequest($"User {userAccountDto.Username} already exists.");
            }

            var userAccountToCreate = new UserAccount
            {
                Username = userAccountDto.Username,
                CreatedOn = DateTime.Now
            };

            var createdUserAccount = repository.Register(userAccountToCreate, userAccountDto.Password);

            this.logger.LogInformation($"PostUserAccount successfully executed.");
            return StatusCode(201, $"Account for user {userAccountDto.Username} created.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserAccountDto userAccountDto)
        {
            var userAccount = await repository.Login(userAccountDto.Username, userAccountDto.Password);

            if (userAccount == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccount.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }

    }
}
