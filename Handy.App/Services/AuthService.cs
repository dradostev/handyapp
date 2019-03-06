using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Handy.Domain.AccountContext.Commands;
using Handy.Domain.AccountContext.Entities;
using Handy.Domain.AccountContext.Services;
using Handy.Domain.SharedContext.Exceptions;
using Handy.Domain.SharedContext.Services;
using Microsoft.IdentityModel.Tokens;

namespace Handy.App.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<Account> _accountRepository;

        public AuthService(IRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<string> GetToken(LogIn command)
        {
            var identity = await GetIdentity(command.Login, command.Password);
            if (identity == null) throw new AccessDeniedException("Incorrect credentials");
            
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("APP_URL"),
                audience: Environment.GetEnvironmentVariable("APP_URL"),
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromHours(int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRATION_TIME")))),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECURITY_KEY"))),
                    SecurityAlgorithms.HmacSha256
                )
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private async Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            var account = await _accountRepository.GetByCriteria(x =>
                x.Login == login && x.Password == PasswordHelper.HashPassword(password));

            if (account == null) return null;
            
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.Id.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}