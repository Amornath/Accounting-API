 using ScopoERP.Models;
using ScopoERP.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ScopoERP.Configuration;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using ScopoERP.Helpers;
using Microsoft.Data.SqlClient;

namespace ScopoERP.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        private readonly Token _token;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private ERPContext _db;

        public UserService(ERPContext db,UserManager<User> userManager, IOptionsMonitor<Token> optionsMonitor, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _token = optionsMonitor.CurrentValue;
            _roleManager = roleManager;
            _mapper = mapper;
            _db = db;
        }

        public async Task<TokenViewModel> Authorize(LoginViewModel loginVM)
        {
            //var user = await _userManager.FindByEmailAsync(loginVM.Email);
            var user = await _userManager.Users.Where(w => w.Email == loginVM.Email && w.PasswordHash == loginVM.Password).FirstOrDefaultAsync();

            if (user != null)
            {
                var Token = new TokenViewModel()
                {
                    Access_Token = await GenerateJwtTokenAsync(loginVM.Email)                  
                };
               
                return Token;
            }
            else
            {
                var Token = new TokenViewModel()
                {
                    Access_Token = "Error"
                };
                return Token;
            }
        }

        public async Task<UserViewModel> GetByUserName(string email)
        {
            var user = await _userManager.Users.Where(w => w.Email == email).FirstOrDefaultAsync();
            var data = _mapper.Map<UserViewModel>(user);
            return data;
        }

        private async Task<string> GenerateJwtTokenAsync(string Email)
        {
            var user = await _userManager.Users.Where(w=>w.Email== Email).Select(u=> new { u.Email, u.UserName, u.Id }).FirstOrDefaultAsync();
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_token.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        public async Task<IdentityResult> AssignToRole(string email, IList<string> Roles)
        {
            return await _userManager.AddToRolesAsync(await _userManager.FindByEmailAsync(email), Roles);
        }

        public async Task<IdentityResult> RetainFromRole(string userName, IList<string> Roles)
        {
            return await _userManager.RemoveFromRolesAsync(await _userManager.FindByNameAsync(userName), Roles);
        }

        public async Task<IEnumerable<IdentityRole>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IList<string>> GetRolesOfUser(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUser(int companyid)
        {
            var userVM = new List<UserViewModel>();
            var data=await _userManager.Users.Where(w=>w.CompanyId==companyid).ToListAsync();
            if (data != null){
                foreach(var user in data)
                {
                   
                    var role = await _userManager.GetRolesAsync(user);
                    userVM.Add(new UserViewModel {ID=user.Id, FirstName = user.FirstName, LastName = user.LastName,Email=user.Email, UserName = user.UserName, StringRoles = string.Join(",", role),Roles=role });
                }
            }
            return userVM;
           
        }

        public async Task<string> ResetPassword(string userId, string oldPassword,string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var data = await _userManager.CheckPasswordAsync(user, oldPassword);
            if(data == true)
            {
                var hashPassword = _userManager.PasswordHasher.HashPassword(user, newPassword);
                user.PasswordHash = hashPassword;
                await _userManager.UpdateAsync(user);
                return "Reset Succecfully!";
            }

            return "Reset Failed!";

        }
        public async Task<int> ResetComppany(string userId,int companyid)
        {
            var user = await _userManager.FindByIdAsync(userId);         
            if (user != null)
            {      
                user.CompanyId = companyid;
                await _userManager.UpdateAsync(user);
                return 1;
            }

            return 0;

        }

    }
}
