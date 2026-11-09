using FlottaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlottaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoforController : ControllerBase
    {
        private FlottaContext _context;

        public SoforController(FlottaContext context)
        {
            _context = context;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllSofor()
        {
            try
            {
                var soforok = _context.Sofors.ToList();
                return Ok(soforok);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet("GetSoforById/{id}")]
        public IActionResult GetSoforById(int id)
        {
                try
                {
                    var sofor = _context.Sofors.FirstOrDefault(s => s.Id == id);
                    if (sofor == null)
                    {
                        return NotFound();
                    }
                    return Ok(sofor);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Something went wrong: {ex.Message}");
                }
        }

        [HttpPost("PostSofor")]
        public IActionResult PostSofor(Sofor sofor)
        {
            try
            {
                if (sofor == null)
                {
                    return BadRequest("Sofor object is null");
                }
                _context.Sofors.Add(sofor);
                _context.SaveChanges();
                return Ok("sikeres hozzáadás");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong: {ex.Message}");
            }
        }

        [HttpPut("PutSofor/{id}")]
        public IActionResult PutSofor(int id)
        {
            try
            {
                Sofor sofor = _context.Sofors.FirstOrDefault(o => o.Id == id);
                if (sofor == null)
                {
                    return NotFound("Nem talalhato sofor");
                }
                _context.Sofors.Update(sofor);
                _context.SaveChanges();
                return Ok("Sikeres frissites");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong: {ex.Message}");
            }
        }

        [HttpDelete("DeleteSofor/{id}")]
        public IActionResult DeleteSofor(int id)
        {
            try
            {
                Sofor soforok = _context.Sofors.FirstOrDefault(o => o.Id == id);
                if (soforok != null)
                {
                    _context.Sofors.Remove(soforok);
                    _context.SaveChanges();
                    return Ok("Sikeres torles");
                }
                else
                {
                    return NotFound("Nem talalhato sofor");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong: {ex.Message}");
            }
        }

    }
}
