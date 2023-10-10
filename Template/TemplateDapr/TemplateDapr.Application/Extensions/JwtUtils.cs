using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Extensions
{
    public class JwtUtils : IJwtUtils
    {
        public string GenerateJwtToken(User user)
        {
            // generate token that is valid for 15 minutes
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("5E8766AECD5E46E888D974FD92B9EAAA");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("UserId", user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.FirstName),
                new Claim(JwtRegisteredClaimNames.Email,user.FirstName),
                new Claim("Name",user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName,user.LastName),
                new Claim(JwtRegisteredClaimNames.NameId,user.UserId)
            }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                NotBefore = DateTime.UtcNow,//Token üretildikten ne kadar süre sonra devreye girsin ayarlıyouz.
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            var refreshToken = new RefreshToken
            {
                RefreshTokenId = Guid.NewGuid().ToString(),
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(1),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
            return refreshToken;
        }

        public int? ValidateJwtToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}