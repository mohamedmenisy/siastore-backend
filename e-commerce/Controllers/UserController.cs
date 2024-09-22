using e_commerce.Context;
using e_commerce.DTO;
using e_commerce.Errors;
using e_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManger;
        private readonly IConfiguration configuration;
        private readonly StoreContext store;

        public UserController(UserManager<ApplicationUser> _userManger,IConfiguration _configuration, StoreContext _store)
        {
            this.userManger = _userManger;
            this.configuration = _configuration;
            store = _store;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto UserData)
        {
            ApplicationUser useremail = await userManger.FindByEmailAsync(UserData.Email);
            if (useremail != null)
            {
                return BadRequest(new ErrorResponse() { Message = "Email Already Exists" });
            }

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Email = UserData.Email;
                user.UserName = UserData.UserName;
                user.PhoneNumber = UserData.Phone;
                user.Name= UserData.Name;

               IdentityResult result = await userManger.CreateAsync(user,UserData.Password);
                if (result.Succeeded)
                {
                    UserCart cart = new UserCart() { UserId = user.Id ,TotalPrice = 0 ,TotalQuantity=0 , CartItems = new List<CartItems>()};
                    Favorite favorite = new Favorite()
                    {
                        UserId = user.Id,
                        FavItems=new List<FavItem>()
                        
                    };
                    store.Favorites.Add(favorite);
                    store.UserCarts.Add(cart);
                    store.SaveChanges();
                    return Ok(new {message= "Account Created" });
                }
                else
                {
                    var list = new List<IdentityError>();
                    foreach (var item in result.Errors)
                    {
                        list.Add(item);
                    }
                    return BadRequest(list);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO userData) {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManger.FindByEmailAsync(userData.Email);
                if (user != null)
                {
                    var result = await   userManger.CheckPasswordAsync(user,userData.Password);
                    if (result == true)
                    {
                        var myclaims = new List<Claim>()
                        {
                            new Claim("UserID",user.Id),
                            new Claim("UserName",user.UserName),
                            new Claim("Name",user.Name),
                            new Claim("Phone",user.PhoneNumber),
                            new Claim("Email",user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

                        };
                        var expDate = DateTime.Now.AddDays(2);
                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
                        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken token = new JwtSecurityToken(
                            issuer: configuration["JWT:ValidIssuer"] ,
                            audience: configuration["JWT:ValidAudience"],
                            claims: myclaims,
                            expires: expDate,
                            signingCredentials: credentials

                            );

                        return Ok(new { message = user.Name , Token = new JwtSecurityTokenHandler().WriteToken(token) , exception = token.ValidTo});
                    }
                    else
                    {
                        return Unauthorized(new ErrorResponse() { Message = "Password is invalid" });

                    }
                }
                return Unauthorized(new ErrorResponse() { Message = "Email NoT Found" });
            }
            else
            {
                var list = new List<ErrorResponse>();
                foreach (var item in ModelState)
                {
                    var errors = item.Value.Errors;
                    foreach (var err in errors)
                    {
                        list.Add(new ErrorResponse() { Message = err.ErrorMessage});
                    }


                }
                return Unauthorized(list);
            }
        
        
   
        }















    }
}
