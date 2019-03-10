using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Handy.App.Configuration;
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
            var account = await _accountRepository.GetByCriteria(x =>
                x.Login == command.Login && x.Password == PasswordHelper.HashPassword(command.Password));
            if (account == null) throw new AccessDeniedException("Incorrect credentials");
            var identity = GetIdentity(account);
            return new JwtSecurityTokenHandler().WriteToken(JwtSecurityTokenFactory(identity));
        }

        public async Task<string> GetToken(LogInViaTelegram command)
        {
            var account = await _accountRepository.GetByCriteria(x => 
                x.Login == command.Login && x.BotChatId == command.ChatId);
            if (account == null)
            {
                account = new Account(
                    command.Login, PasswordHelper.GetRandomString(8), 3, command.ChatId, command.ScreenName
                );
                await _accountRepository.Persist(account);
            }
            var identity = GetIdentity(account);
            return new JwtSecurityTokenHandler().WriteToken(JwtSecurityTokenFactory(identity));
        }

        private ClaimsIdentity GetIdentity(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.Id.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
        
        private static JwtSecurityToken JwtSecurityTokenFactory(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            return new JwtSecurityToken(
                issuer: AppConfig.AppUrl,
                audience: AppConfig.AppUrl,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromHours(AppConfig.JwtExpirationTime)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppConfig.JwtSecurityKey)),
                    SecurityAlgorithms.HmacSha256
                )
            );
        }
    }
}