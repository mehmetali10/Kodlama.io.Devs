using Application.Features.GithubProfiles.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Commands.CreateGithubProfile
{
    public class CreateGithubProfileCommandValidator : AbstractValidator<CreatedGithubProfileDto>
    {
        public CreateGithubProfileCommandValidator()
        {
            RuleFor(g => g.RepoUrl).NotEmpty();
            RuleFor(g => g.RepoName).NotEmpty();

        }
    }
}
