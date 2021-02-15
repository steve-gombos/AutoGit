using AutoGit.Core.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace AutoGit.Core.Services
{
    public class AccessTokenFactory : IAccessTokenFactory
    {
        public string Create(string appIdentifier, string privateKey)
        {
            var key = RSA.Create();
            key.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new RsaSecurityKey(key);

            var token = tokenHandler.CreateJwtSecurityToken(
                issuer: appIdentifier,
                issuedAt: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256));

            return tokenHandler.WriteToken(token);
        }
    }
}