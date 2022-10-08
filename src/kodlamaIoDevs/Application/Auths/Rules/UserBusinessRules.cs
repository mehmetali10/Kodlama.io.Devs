using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Rules
{
    public class UserBusinessRules
    {
        public IUserRepository _userRepository { get; set; }

        public UserBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task UserAccountCanNotBeDuplicatedWhenInserted(string email)
        {
            IPaginate<User> result = await _userRepository.GetListAsync(b => b.Email == email);
            if (result.Items.Any()) throw new BusinessException("User Account Mail exists.");
        }


        public async Task UserShouldExistWhenRequested(User user)
        {
            if (user == null) throw new BusinessException("User Email does not exists");
        }


        public async Task VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (!HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt))
                throw new BusinessException("Pasword Error");
        }

        public async Task EmailCannotBeDuplicatedWhenRegistered(string email)
        {
            User? user = await _userRepository.GetAsync(u => u.Email == email);
            if (user != null) throw new BusinessException("Mail already exits");
        }
    }
}
