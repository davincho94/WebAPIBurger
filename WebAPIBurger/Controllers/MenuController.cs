using Microsoft.AspNetCore.Mvc;

namespace WebAPIBurger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var sandwiches = new List<string> { "X Burger – $ 5,00", "X Egg - $ 4,50", "X Bacon - $ 7,00" };
            var extras = new List<string> { "Fries - $ 2,00", "Soft drink - $ 2,50" };

            return Ok(new { Sandwiches = sandwiches, Extras = extras });
        }


        [HttpGet("sandwiches")]
        public ActionResult<IEnumerable<string>> GetSandwiches()
        {
            var sandwiches = new List<string> { "X Burger – $ 5,00", "X Egg - $ 4,50", "X Bacon - $ 7,00" };

            return Ok(sandwiches);
        }

        [HttpGet("extras")]
        public ActionResult<IEnumerable<string>> GetExtras()
        {
            var extras = new List<string> { "Fries - $ 2,00", "Soft drink - $ 2,50" };

            return Ok(extras);
        }
    }
}
