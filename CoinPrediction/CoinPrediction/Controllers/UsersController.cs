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
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = userRepository.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var users = userRepository.GetUserByID(id);
            return Ok(users);
        }

        [HttpPost]
        public ActionResult<Guid> Post([FromBody] User user)
        {
            try
            {
                User created = userRepository.InsertUser(user);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<User> AddAssetToUser(int id, [FromBody] UserAsset asset) 
        {
            var user = userRepository.AddAssetToUser(id, asset);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        //[HttpPut]
        //public ActionResult<User> Put([FromBody] User user)
        //{
        //    var updated = userRepository.UpdateUser(user);

        //    if (updated == null)
        //        return NotFound();

        //    return Ok(updated);
        //}

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var isSuccess = userRepository.DeleteUser(id);

            if (!isSuccess)
                return NotFound();

            return NoContent();
        }

    }
}
