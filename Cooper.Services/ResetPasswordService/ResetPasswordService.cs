using Cooper.Models;
using Cooper.Repositories;
using Cooper.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace Cooper.Services
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly UserRepository userRepository;
        private readonly ILogger<ResetPasswordService> logger;

        public ResetPasswordService(IJwtHandlerService jwtService, IConfigProvider configProvider, ILogger<ResetPasswordService> logger)
        {
            userRepository = new UserRepository(jwtService, configProvider);
            this.logger = logger;
        }

        public string CreateToken(string email)
        {
            ResetToken resetToken = new ResetToken();
            resetToken.Email = email;
            resetToken.Token = Guid.NewGuid().ToString();
            userRepository.Create(resetToken);
            return resetToken.Token;
        }
    }
}