using Microsoft.AspNetCore.Mvc;
using ScopoERP.ViewModels;
using ScopoERP.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ScopoERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<AuthController> _logger;
       

        public AuthController(UserService userService, ILogger<AuthController> logger, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _logger = logger;
           
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<TokenViewModel>> Login(LoginViewModel loginVM)
        {
            _logger.LogInformation(message: $"Login for {{userVM}}");
            return Ok(await _userService.Authorize(loginVM));
        }

        [HttpPost]
        [Route("AssignToRole")]
        public async Task<ActionResult<TokenViewModel>> RoleAssign(UserViewModel userVM)
        {
            _logger.LogInformation(message: $"Assigning {{userVM}} To {{userVM.Roles}}");
            return Ok(await _userService.AssignToRole(userVM.UserName, userVM.Roles));
        }

        [HttpGet("{Email}")]
        public async Task<ActionResult<UserViewModel>> Get(string email)
        {
            _logger.LogInformation(message: $"Get Data of {{Email}}");
            return Ok(await _userService.GetByUserName(email));
        }

        [HttpGet]
        public async Task<ActionResult<List<UserViewModel>>> Get()
        {
            var companyid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation(message: $"Get All Users");
            return Ok(await _userService.GetAllUser(Convert.ToInt32(companyid)));
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<ActionResult<string>> ResetPassword(string userId, string oldPassword, string newPassword)
        {
          var result= await _userService.ResetPassword(userId, oldPassword, newPassword);

            return Ok(result);

        }
    }
}
