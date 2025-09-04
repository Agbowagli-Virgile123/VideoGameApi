using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VideoGameApi.Data;
using VideoGameApi.Interfaces;
using VideoGameApi.Models;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.User;
using YamlDotNet.Core.Tokens;

namespace VideoGameApi.Services
{
    public class UserServices : IUser
    {

        private readonly VideoGameDbContext _context;
        private readonly IConfiguration _configuration;
        public UserServices(VideoGameDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<(MdResponse,User)> RegisterUser(MdUser request)
        {
            MdResponse response = new MdResponse { ResponseCode = 0};
            User user = new();

            if(request == null)
            {
                response.ResponseMessage = "All fields are required";
                return (response, null!);
            }

            //Check if there is any user with that user name
            var check = await _context.Users.AnyAsync(u => u.UserName == request.UserName);

            if(check)
            {
                response.ResponseMessage = "User name already exists";
                return (response, null!);
            }

            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password); 

            user.UserName = request.UserName;
            user.HashedPassword = hashedPassword;
            user.Role = request.Role;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            response.ResponseCode = 1;
            response.ResponseMessage = "User Created Successfully";

            return (response,user);
        }

        public async Task<(MdResponse, string, string, User)> LogInUser(MdUser cred)
        {
            MdResponse response = new MdResponse { ResponseCode = 0};
            string refreshToken = string.Empty;
            string Token = string.Empty;
            User? user = new User();

            if (cred == null)
            {
                response.ResponseMessage = "Username or Password is required";
                return (response, refreshToken,Token, null!) ;
            }


            user = await _context.Users.Where(u => u.UserName == cred.UserName).FirstOrDefaultAsync();

            if (user is null)
            {
                response.ResponseMessage = "Invalid credentials";
                user = new();
                return (response, refreshToken, Token, null!);
            }


            var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.HashedPassword, cred.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                response.ResponseMessage =  "Invalid credentials";
                user = new();
                return (response, refreshToken, Token, null!);
            }


            Token = CreateJwtToken(user);
            refreshToken = await GenerateAndSaveRefreshTokenAsync(user);

            user.HashedPassword = "";

            response.ResponseCode = 1;
            response.ResponseMessage = "User Logged in Successfully";

            return (response, refreshToken, Token, user);
        }
        
        public async Task<(string, string)> RefreshTokensAsync(MdRefreshTokenRequest request)
        {
            if(request is null || string.IsNullOrEmpty(request.RefreshToken) || request?.UserId == null)
            {
                return (null!, null!);
            }

            var user = await ValidateRefreshToken(request.UserId, request.RefreshToken!);
            if(user is null)
            {
                return (null!, null!);
            }

            string Token = CreateJwtToken(user);
            string refreshToken = await GenerateAndSaveRefreshTokenAsync(user);

            return (Token, refreshToken);
        }

        //Method the refresh the access token based on the UserId and the refresh 
        private async Task<User> ValidateRefreshToken(Guid userId,  string refreshToken)
        {
            var user = await _context.Users.FindAsync(userId);

            if(user is null || string.IsNullOrEmpty(user.RefreshToken) || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <  DateTime.UtcNow)
            {
                return null!;
            }

            return user;
        }

        //Method to create the refresh token
        private string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        //Function to generate and save the refresh token
        private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = CreateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _context.SaveChangesAsync();

            return user.RefreshToken;
        }
      
        //Method to create the JWT Token
        private string CreateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            //Install-Package System.IdentityModel.Tokens.Jwt
            var key =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:SecretKey")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken
            (
                issuer: _configuration.GetValue<string>("JWT:Issuer"),
                audience: _configuration.GetValue<string>("JWT:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration.GetValue<double>("JWT:Expiration"))),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}
