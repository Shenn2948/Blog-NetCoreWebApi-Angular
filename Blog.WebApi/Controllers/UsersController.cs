using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Blog.DataAccess.Entities.Identity;
using Blog.Services.Users.Requests;
using Blog.Services.Users.Responses;
using Blog.WebApi.Helpers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IConfiguration _configuration;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // GET api/users/userdata
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> UserData()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            var userData = new UserDataResponse { Id = user.Id, Name = user.UserName, Email = user.Email };
            return Ok(userData);
        }

        // POST api/users/register
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterEntity model)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return BadRequest(errorMessage);
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(user, false);
            var token = AuthenticationHelper.GenerateJwtToken(user, _configuration);

            var rootData = new SignUpResponse(token, user.UserName, user.Email, user.Id);
            return Ok(rootData);
        }

        // POST api/users/login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginEntity model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var token = AuthenticationHelper.GenerateJwtToken(user, _configuration);

                    var rootData = new LoginResponse(token, user.UserName, user.Email, user.Id);
                    return Ok(rootData);
                }

                return StatusCode((int)HttpStatusCode.Unauthorized, "Bad Credentials");
            }

            string errorMessage = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return BadRequest(errorMessage);
        }
    }
}