using Application.Auths.Dtos;
using Application.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auths.Command.Register
{
    public class RegisterUserCommand : IRequest<RegisteredDto>
    {

        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; }


        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisteredDto>
        {

            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly IAuthService _authService;


            public RegisterUserCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules, IAuthService authService)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _userBusinessRules = userBusinessRules;
                _authService = authService;
            }

            async Task<RegisteredDto> IRequestHandler<RegisterUserCommand, RegisteredDto>.Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.EmailCannotBeDuplicatedWhenRegistered(request.UserForRegisterDto.Email);

                byte[] passwordHash, passwordSalt;

                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);
                
                User user = _mapper.Map<User>(request);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Status = true;
                user.AuthenticatorType = AuthenticatorType.Email;

                //await _userBusinessRules.UserAccountCanNotBeDuplicatedWhenInserted(request.Email);

                User createdUser = await _userRepository.AddAsync(user);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);

                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAddress);

                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                RegisteredDto registeredDto = new()
                {
                    RefreshToken = addedRefreshToken,
                    AccessToken = createdAccessToken,
                };

                AccessToken accessToken = _tokenHelper.CreateToken(createdUser, new List<OperationClaim>());

                return registeredDto;
            }
        }
    }
}
