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

namespace Application.Features.GithubProfiles.Commands.UpdateGithubProfile
{
    public class UpdateGithubProfileCommand : IRequest<UpdatedGithubProfileDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RepoName { get; set; }
        public string RepoUrl { get; set; }

        public class UpdateGithubProfileCommandHandler : IRequestHandler<UpdateGithubProfileCommand, UpdatedGithubProfileDto>
        {
            private readonly IGithubPrfoileRepository _githubPrfoileRepository;
            private readonly IMapper _mapper;
            private readonly GithubProfileBusinessRules _githubProfileBusinessRules;

            public UpdateGithubProfileCommandHandler(IGithubPrfoileRepository githubPrfoileRepository, IMapper mapper, GithubProfileBusinessRules githubProfileBusinessRules)
            {
                _githubPrfoileRepository = githubPrfoileRepository;
                _mapper = mapper;
                _githubProfileBusinessRules = githubProfileBusinessRules;
            }

            public async Task<UpdatedGithubProfileDto> Handle(UpdateGithubProfileCommand request, CancellationToken cancellationToken)
            {
                GithubProfile? githubProfile = await _githubPrfoileRepository.GetAsync(g => g.Id == request.Id);
                await _githubProfileBusinessRules.GithubProfileShouldExistWhenRequested(githubProfile);

                GithubProfile updateGithubProfile = await _githubPrfoileRepository.UpdateAsync(_mapper.Map(request, githubProfile));

                UpdatedGithubProfileDto updatedGithubProfileDto = _mapper.Map<UpdatedGithubProfileDto>(updateGithubProfile);

                return updatedGithubProfileDto;
            }
        }
    }
}
