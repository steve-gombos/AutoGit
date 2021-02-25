using AutoGit.Core.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace AutoGit.Core.Services
{
    public class AccessTokenFactory : IAccessTokenFactory
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public AccessTokenFactory(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public string Create(string appIdentifier, string privateKey)
        {
            var key = RSA.Create();
            key.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new RsaSecurityKey(key);

            var token = tokenHandler.CreateJwtSecurityToken(
                appIdentifier,
                notBefore: _dateTimeProvider.UtcNow,
                issuedAt: _dateTimeProvider.UtcNow,
                expires: _dateTimeProvider.UtcNow.AddMinutes(10),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256));

            return tokenHandler.WriteToken(token);
        }
    }
}