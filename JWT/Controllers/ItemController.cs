using JWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly Iitems _items;

        public ItemController(Iitems items)
        {
            _items = items;
        }

        [HttpPost("createTask")]
        [Authorize]
        public ActionResult createtask(Items item)
        {
            try
            {
                if(item == null)
                {
                    return BadRequest("give proper informations");
                }
                item.Id = _items.Alltask().LastOrDefault().Id + 1;
                _items.createtask(item);
                return Ok("saved successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"something went wrong {ex.Message}");
            }

            
        }


        [HttpGet]
        [Authorize]
        public IActionResult getbyid(int Id)
        {
            try
            {
                if(Id == 0)
                {
                    return BadRequest("invalid Id");
                }
                return Ok(_items.getbyid(Id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"internal server error {ex.Message}");
            }
        }

        [HttpPut("updatetask")]
        [Authorize]
        public IActionResult update (int Id, [FromBody] Items item)
        {
            try
            {
                if(Id == 0)
                {
                    return BadRequest("invalid Id");
                }
                _items.updatetask(Id, item);
                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"internal server error {ex.Message}");
            }

        }
        [Authorize(Roles = "admin")]
        [HttpGet("getall")]
        public IActionResult getall()
        {
            return Ok(_items.Alltask());
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("deletetask{Id:int}")]
        public IActionResult removetask(int Id)
        {
            _items.deletetask(Id);
            return NoContent();
        }
    }
}
