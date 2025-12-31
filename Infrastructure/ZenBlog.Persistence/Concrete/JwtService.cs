using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Contracts.Persistance;
using ZenBlog.Application.Features.User.Results;
using ZenBlog.Application.Options;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Persistence.Concrete
{
    public class JwtService(UserManager<AppUser> _userManager, IOptions<JwtTokenOptions> tokenOptions) : IJwtService
    {
        private readonly JwtTokenOptions _jwtTokenOptions = tokenOptions.Value;
        public async Task<GetLoginQueryResult> GenerateTokenAsync(GetUserQueryResult result)
        {
            var user = await _userManager.FindByNameAsync(result.UserName);

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenOptions.Key));

            var dateTimeNow = DateTime.UtcNow;

            List<Claim> claims = new()
            {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("fullName", string.Join(",", user.FirstName, user.LastName)),
            };

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtTokenOptions.Issuer,
                audience: _jwtTokenOptions.Audience,
                claims: claims,
                notBefore: dateTimeNow,
                expires: dateTimeNow.AddMinutes(_jwtTokenOptions.ExpireInMinutes),
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );

            GetLoginQueryResult response = new();

            response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.ExpirationTime = dateTimeNow.AddMinutes(_jwtTokenOptions.ExpireInMinutes);

            return response;
        }
    }
}