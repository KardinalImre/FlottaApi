using FlottaApi.DTOs;
using FlottaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Argon2Sharp;

namespace FlottaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private FlottaContext _context;

        public LoginController(FlottaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                User user = _context.Users.FirstOrDefault(u => u.LoginName == loginDTO.LoginName);
                if (user == null)
                {
                    return NotFound("Missing user");
                }
                string phcstring = user.Hash;
                var (isValid, Settings) = Argon2PhcFormat.VerifyPhcString(loginDTO.Password, phcstring);
                if (isValid)
                {
                    return Ok("token");
                }
                return BadRequest("Wrong password");
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong happened: " + ex.Message);
            }
        }
    }
}
