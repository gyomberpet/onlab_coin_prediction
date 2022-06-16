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

        /// <summary>
        /// Get all the users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await userRepository.GetUsers();
            return Ok(users);
        }

        /// <summary>
        /// Get a user with the given id.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var users = await userRepository.GetUserByID(id);
            return Ok(users);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add the given asset to the user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asset"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> AddAssetToUser(int id, [FromBody] UserAsset asset) 
        {
            var user = await userRepository.AddAssetToUser(id, asset);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Delete a user with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
