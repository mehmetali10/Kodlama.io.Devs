using Application.Features.GithubProfiles.Commands.CreateGithubProfile;
using Application.Features.GithubProfiles.Commands.DeleteGithubProfile;
using Application.Features.GithubProfiles.Commands.UpdateGithubProfile;
using Application.Features.GithubProfiles.Dtos;
using Application.Features.GithubProfiles.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GithubProfile, CreatedGithubProfileDto>().ReverseMap();
            CreateMap<GithubProfile, CreateGithubProfileCommand>().ReverseMap();

            CreateMap<GithubProfile, DeletedGithubProfileDto>().ReverseMap();
            CreateMap<GithubProfile, DeleteGithubProfileCommand>().ReverseMap();

            CreateMap<GithubProfile, UpdatedGithubProfileDto>().ReverseMap();
            CreateMap<GithubProfile, UpdateGithubProfileCommand>().ReverseMap();

            CreateMap<IPaginate<GithubProfile>, GithubProfileListModel>().ReverseMap();
            CreateMap<GithubProfile, GithubProfileListDto>().ReverseMap();
        }
    }
}
