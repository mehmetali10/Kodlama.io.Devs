using Application.Features.Users.Rules;
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

namespace Application.Features.Users.Commands.Register
{
    public class RegisterUserCommand : UserForRegisterDto, IRequest<AccessToken>
    {
        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly UserBusinessRules _userBusinessRules;


            public RegisterUserCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _userBusinessRules = userBusinessRules;

            }


            async Task<AccessToken> IRequestHandler<RegisterUserCommand, AccessToken>.Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                byte[] passwordHash, passwordSalt;

                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                
                User user = _mapper.Map<User>(request);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Status = true;
                user.AuthenticatorType = AuthenticatorType.Email;

                await _userBusinessRules.UserAccountCanNotBeDuplicatedWhenInserted(request.Email);
                User createdUser = await _userRepository.AddAsync(user);

                AccessToken accessToken = _tokenHelper.CreateToken(createdUser, new List<OperationClaim>());

                return accessToken;
            }
        }
    }
}
