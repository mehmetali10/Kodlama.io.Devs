using Application.Features.GithubProfiles.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GithubProfiles.Models
{
    public class GithubProfileListModel
    {
        public ICollection<GithubProfileListDto> Items { get; set; }
    }
}
