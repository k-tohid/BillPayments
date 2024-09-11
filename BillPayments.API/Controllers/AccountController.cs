using BillPayments.Application.DTOs;
using BillPayments.Application.Interfaces;
using BillPayments.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BillPayments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IJwtService jwtService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }



        #region Register
        /// <summary>
        /// endpoint for registering users with username, email and password
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> PostRegister(RegisterDTO registerDTO)
        {
            var newUser = new ApplicationUser { UserName = registerDTO.UserName, Email = registerDTO.Email };
            var result = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if (result.Succeeded)
            {
                //await _signInManager.SignInAsync(newUser, isPersistent: true);
                var authResponse = _jwtService.CreateJwtToken(newUser);
                return Ok(authResponse);
            }

            return Problem(
                title: "User creation failed",
                detail: string.Join(", ", result.Errors.Select(e => e.Description)),
                statusCode: 400
                );
        }

        #endregion



        #region Login
        /// <summary>
        /// endpoint to login users
        /// </summary>
        /// <param name="signInDTO">userName and password</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> PostLogin(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.UserName);

            if (user == null)
            {
                return Problem("No user with that userName");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var authToken = _jwtService.CreateJwtToken(user);

                return Ok(authToken);
            }

            return Problem("Invalid username or password");
        }

        #endregion

        [Authorize]
        [HttpGet("AdminHello")]
        public async Task<ActionResult> GetAdminHello()
        {

            return Ok("Admin Says Hello");
        }


        [HttpGet("UserHello")]
        public async Task<ActionResult> GetUserHello()
        {

            return Ok("Anonymous Says Hello");
        }
    }
}
