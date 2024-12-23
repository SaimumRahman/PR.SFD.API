﻿using JM.AuthServer.API.Models;
using JM.AuthServer.API.Services.RefreshTokenRepositories;
using JM.AuthServer.API.Services.TokenGenerators;
using System.Threading.Tasks;

namespace JM.AuthServer.API.Services.Authenticators
{
    public class Authenticator
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public Authenticator(AccessTokenGenerator accessTokenGenerator,
            RefreshTokenGenerator refreshTokenGenerator,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthenticatedUserResponse> Authenticate(User user)
        {
            AccessToken accessToken = _accessTokenGenerator.GenerateToken(user);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

            RefreshToken refreshTokenDTO = new RefreshToken()
            {
               Token = refreshToken,
                UserId = user.Id
            };
             await _refreshTokenRepository.CreaterRefreshToken(refreshTokenDTO);
            bool a ;
           
            return new AuthenticatedUserResponse()
            {
                Token = accessToken.Value,
                ExpireDate = accessToken.ExpirationTime,
                RefreshToken = refreshToken,
                UserId = user.Id,
                Username = user.UserName,
                Id = user.Id.ToString()
              //Roles = await _refreshTokenRepository.GetRolesByUserId(user.UserId)


            };
        }
    }
}
