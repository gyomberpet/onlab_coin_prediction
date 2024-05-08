using CoinPrediction.DAL;
using CoinPrediction.Model;
using Microsoft.AspNetCore.Mvc;

namespace CoinPrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await userRepository.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var users = await userRepository.GetUserByID(id);
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] User user)
        {
            try
            {
                User created = await userRepository.InsertUser(user);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> AddAssetToUser(int id, [FromBody] UserAsset asset) 
        {
            var user = await userRepository.AddAssetToUser(id, asset);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var isSuccess = await userRepository.DeleteUser(id);

            if (!isSuccess)
                return NotFound();

            return NoContent();
        }

    }
}
