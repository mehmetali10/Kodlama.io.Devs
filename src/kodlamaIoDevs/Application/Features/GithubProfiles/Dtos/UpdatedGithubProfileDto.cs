using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Dtos
{
    public class UpdatedGithubProfileDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RepoName { get; set; }
        public string RepoUrl { get; set; }
    }
}
