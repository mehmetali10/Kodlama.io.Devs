using Application.Features.GithubProfiles.Dtos;
using Application.Features.GithubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Commands.DeleteGithubProfile
{
    public class DeleteGithubProfileCommand : IRequest<DeletedGithubProfileDto>
    {
        public int Id { get; set; }

        public class DeleteGithubProfileCommandHandler : IRequestHandler<DeleteGithubProfileCommand, DeletedGithubProfileDto>
        {
            private readonly IGithubPrfoileRepository _githubPrfoileRepository;
            private readonly IMapper _mapper;
            private readonly GithubProfileBusinessRules _githubProfileBusinessRules;

            public DeleteGithubProfileCommandHandler(IGithubPrfoileRepository githubPrfoileRepository, IMapper mapper, GithubProfileBusinessRules githubProfileBusinessRules)
            {
                _githubPrfoileRepository = githubPrfoileRepository;
                _mapper = mapper;
                _githubProfileBusinessRules = githubProfileBusinessRules;
            }

            public async Task<DeletedGithubProfileDto> Handle(DeleteGithubProfileCommand request, CancellationToken cancellationToken)
            {
                GithubProfile? githubProfile = await _githubPrfoileRepository.GetAsync(g => g.Id == request.Id);

                await _githubProfileBusinessRules.GithubProfileShouldExistWhenRequested(githubProfile);

                GithubProfile deltedGithubProfile = await _githubPrfoileRepository.DeleteAsync(githubProfile);
                DeletedGithubProfileDto mappedGithubProfileDto = _mapper.Map<DeletedGithubProfileDto>(deltedGithubProfile);
                return mappedGithubProfileDto;

            }
        }
    }
}
