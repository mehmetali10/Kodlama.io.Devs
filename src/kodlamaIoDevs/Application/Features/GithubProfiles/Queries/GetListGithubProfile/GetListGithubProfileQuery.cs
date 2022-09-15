using Application.Features.GithubProfiles.Models;
using Application.Features.GithubProfiles.Rules;
using Application.Features.Technologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Queries.GetListGithubAccount
{
    public class GetListGithubProfileQuery  :IRequest<GithubProfileListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListGithubProfileQueryHandler : IRequestHandler<GetListGithubProfileQuery, GithubProfileListModel>
        {
            private readonly IGithubPrfoileRepository _githubPrfoileRepository;
            private readonly IMapper _mapper;
            private readonly GithubProfileBusinessRules _githubProfileBusinessRules;

            public GetListGithubProfileQueryHandler(IGithubPrfoileRepository githubPrfoileRepository, IMapper mapper, GithubProfileBusinessRules githubProfileBusinessRules)
            {
                _githubPrfoileRepository = githubPrfoileRepository;
                _mapper = mapper;
                _githubProfileBusinessRules = githubProfileBusinessRules;
            }

            public async Task<GithubProfileListModel> Handle(GetListGithubProfileQuery request, CancellationToken cancellationToken)
            {
                IPaginate<GithubProfile> githubProfiles = await _githubPrfoileRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                GithubProfileListModel mappedGithubProfileListModel = _mapper.Map<GithubProfileListModel>(githubProfiles);
                return mappedGithubProfileListModel;
            }
        }
    }
}
