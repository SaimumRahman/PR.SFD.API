
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using MySqlX.XDevAPI.Common;
using JM.AuthServer.API.Models;
using JM.AuthServer.API.Repository.Auths;
using JM.AuthServer.API.Services.Authenticators;
using JM.AuthServer.API.Services.RefreshTokenRepositories;
using JM.AuthServer.API.Services.TokenValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JM.AuthServer.API.Controllers
{
    [Route("api/auth/")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<User> _userRepository;
        private readonly IAuthRepository _userRepositoryRaw;
        private readonly Authenticator _authenticator;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthenticationController(IAuthRepository userRepositoryRaw, UserManager<User> userRepository,
            Authenticator authenticator,
            RefreshTokenValidator refreshTokenValidator,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepositoryRaw = userRepositoryRaw;
            _userRepository = userRepository;
            _authenticator = authenticator;
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenRepository = refreshTokenRepository;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }
            try
            {
                var exUser = await _userRepositoryRaw.IsUserExists(registerRequest.Username, registerRequest.Email, registerRequest.PhoneNumber);

                if (registerRequest.Password != registerRequest.ConfirmPassword)
                {
                    return BadRequest(new ResponseResult("Password does not matched with confirm password."));
                }

                User registrationUser = new()
                {
                    Email = registerRequest.Email,
                    UserName = registerRequest.Username,
                    LockoutEnd = new DateTimeOffset(),
                    IsFirstLogin = 1,
                    IsActive = 1,
                    PhoneNumber = registerRequest.PhoneNumber,
                    PhoneNumberConfirmed = false,
                    EmailConfirmed = false,
                    FullName = registerRequest.FullName,
                    DesignationId = registerRequest.DesignationId,
                    DepartmentId = registerRequest.DepartmentId,
                    //UserID = registerRequest.UserID,
                    CreateBy = registerRequest.CreateBy,
                    UpdateBy = registerRequest.UpdateBy,
                    LandingPage = registerRequest.LandingPage,
                    ThemeId = registerRequest.ThemeId,
                    LastLoginDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    IsExpired = 0

                };
                IdentityResult result;
                if (exUser == null)
                {
                    registrationUser.IsFirstLogin = 0;

                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password, 14);
                    int results = await _userRepositoryRaw.CreateUser(registrationUser, hashedPassword);

                    //else
                    //{
                    //    result = await _userRepository.UpdateAsync(registrationUser);

                    //}
                    if (results != 1)
                    {
                        //IdentityErrorDescriber errorDescriber = new IdentityErrorDescriber();
                        //IdentityError primaryError = result.Errors.FirstOrDefault();

                        //if (primaryError.Code == nameof(errorDescriber.DuplicateEmail))
                        //{
                        //    return Conflict(new ResponseResult("Email is already exists."));
                        //}
                        //else if (primaryError.Code == nameof(errorDescriber.DuplicateUserName))
                        //{
                        //    return Conflict(new ResponseResult("User Name is already exists."));
                        //}
                        //else
                        //{
                        //    return Conflict(new ResponseResult(result.Errors));

                        //}
                    }
                    else
                    {
                        var rv = new ResponseResult("User has been created succesfully", true);
                        rv.Id = registrationUser.Id.ToString();
                        return Ok(rv);
                    }
                }
                return Conflict(new ResponseResult("User is already exists."));
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseResult(ex.Message, false));

            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequestModelState();
                }

                User user = await _userRepositoryRaw.LoginUser(loginRequest.LoginId);

                // User user = await _userRepository.GetTwoFactorEnabledAsync();


                //if (user == null)
                //{
                //    user = await _userRepository.FindByEmailAsync(loginRequest.LoginId);
                //    //if (user == null)
                //    //    return Unauthorized();
                //}
                //if (user == null)
                //{
                //    user = _userRepository.Users.Where(s => s.PhoneNumber == loginRequest.LoginId).FirstOrDefault();
                //    if (user == null)
                //        return Unauthorized();
                //}
                //var passwordHasher = new PasswordHasher<User>();
                //var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PASSWORDHASH, loginRequest.Password);

               
                if (user != null)
                {

                   var result = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PASSWORDHASH);
                    
                    if (result == false)
                    {
                        return Unauthorized();
                    }
                }

                //bool isCorrectPassword = await _userRepository.CheckPasswordAsync(user, loginRequest.Password);
               

                var response = await _authenticator.Authenticate(user);

                return Ok(response);
            }
            catch (Exception e)
            {

                throw;
            }

        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
            if (!isValidRefreshToken)
            {
                return BadRequest(new ResponseResult("Invalid refresh token."));
            }

            RefreshToken refreshTokenDTO = await _refreshTokenRepository.GetByToken(refreshRequest.RefreshToken);
            if (refreshTokenDTO == null)
            {
                return NotFound(new ResponseResult("Invalid refresh token."));
            }

            await _refreshTokenRepository.DeleteTokenByUserId(refreshTokenDTO.UserId);

            //User user = await _userRepository.FindByIdAsync(refreshTokenDTO.UserId.ToString());
            User user = await _userRepositoryRaw.LoginUserById(refreshTokenDTO.UserId);
            if (user == null)
            {
                return NotFound(new ResponseResult("User not found."));
            }

            AuthenticatedUserResponse response = await _authenticator.Authenticate(user);

            return Ok(response);
        }

        //[Authorize]
        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    string rawUserId = HttpContext.User.FindFirstValue("id");

        //    if (!Guid.TryParse(rawUserId, out Guid userId))
        //    {
        //        return Unauthorized();
        //    }

        //    await _refreshTokenRepository.DeleteAll(userId);

        //    return NoContent();
        //}

        private IActionResult BadRequestModelState()
        {
            IEnumerable<string> errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

            return BadRequest(new ResponseResult(errorMessages));
        }

        [Authorize]
        [HttpGet("User")]
        public async Task<IActionResult> Get(string loginId)
        {
            string rawUserId = HttpContext.User.FindFirstValue("id");

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                // return Unauthorized();
            }

            User user = await _userRepository.FindByNameAsync(loginId);
            user.PasswordHash = "";

            return Ok(user);
        }

        //[Authorize]
        [HttpPost("changePassword")]
        public async Task<IActionResult> PasswordReset(string Id, string oldPassword, string newPassword)
        {
            //var user = await _userRepository.FindByIdAsync(Id);
            var user = await _userRepository.FindByIdAsync(Id);
            StringValues token = "";

            //  HttpContext.Request.Headers.TryGetValue("Authorization", out token);

            if (user != null)
            {
                var result = await _userRepository.ChangePasswordAsync(user, oldPassword, newPassword);
                if (result.Succeeded)
                {
                    return Ok(new ResponseResult("Password has been reset succesfully", true));

                }
                else
                {
                    return Conflict(new ResponseResult(result.Errors));

                }

            }


            return NotFound(new ResponseResult("Not Found", false));
        }
        [HttpPost("forgetPassword")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Forget(string email)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            try
            {
                if (user != null /*&& await _userRepository.IsEmailConfirmedAsync(user)*/)
                {
                    string code = await _userRepository.GeneratePasswordResetTokenAsync(user);
                    var response = new ResponseResult("Code Has been generated succesfully", true);
                    response.Id = code;
                    return Ok(response);

                }

            }
            catch (Exception ex)
            {

                throw;
            }


            return NoContent();
        }
        [HttpPost("ResetPassword")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string email, string token, string newPassword)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            try
            {
                if (user != null /*&& await _userRepository.IsEmailConfirmedAsync(user)*/)
                {
                    var result = await _userRepository.ResetPasswordAsync(user, token, newPassword);
                    if (result.Succeeded)
                    {
                        return Ok(new ResponseResult("Password has been reset succesfully", true));

                    }
                    else
                    {
                        return Conflict(new ResponseResult(result.Errors));

                    }

                }

            }
            catch (Exception ex)
            {

                throw;
            }


            return NoContent();
        }
    }
}
