using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GithubProfile : Entity
    {

        public int UserId { get; set; }
        public string RepoName { get; set; }
        public string RepoUrl { get; set; }

        public GithubProfile()
        {

        }

        public GithubProfile(int id, int userId, string repoName, string repoUrl) : this()
        {
            Id = id;
            UserId = userId;
            RepoName = repoName;
            RepoUrl = repoUrl;
        }
    }

}
