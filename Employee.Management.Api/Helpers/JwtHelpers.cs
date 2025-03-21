﻿using Employees.Management.Models;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Employees.Management.Api.Helpers
{
    public static class JwtHelpers
    {     
        
            public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
            {
                List<Claim> claims = new List<Claim>
            {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.UserName),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyy HH:mm:ss tt")),
                //    new Claim(ClaimTypes.Role, role) // Aquí agregas el claim de rol
            };    
                return claims;
            }
            public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
            {
                Id = Guid.NewGuid(); 
                return GetClaims(userAccounts, Id);
            }
            
            public static UserTokens GenTokenKey(UserTokens model, JwtSettings jwtSettings)
            {
                try
                {
                    var userToken = new UserTokens();
                    if (model == null) 
                    {
                        throw new ArgumentNullException(nameof(model));
                    }
                    
                    var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

                    Guid Id; 

                    DateTime expireTime = DateTime.UtcNow.AddDays(1);
                    
                    userToken.Validity = expireTime.TimeOfDay;
                    
                    var jwtToken = new JwtSecurityToken(                        
                        issuer: jwtSettings.ValidIssuer,
                        audience: jwtSettings.ValidAudience,
                        claims: GetClaims(model, out Id),
                        notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                        expires: new DateTimeOffset(expireTime).DateTime, 
                        signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256));

                    userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                    userToken.UserName = model.UserName;
                    userToken.Id = model.Id;
                    userToken.GuidId = Id;

                    return userToken;
                }
                catch (Exception exception)
                {
                    throw new Exception("Error Generating JWT", exception);
                }

            }
    }
}

