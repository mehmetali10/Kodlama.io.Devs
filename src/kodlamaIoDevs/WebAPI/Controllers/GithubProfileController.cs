using Application.Features.GithubProfiles.Commands.CreateGithubProfile;
using Application.Features.GithubProfiles.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Application.Requests;
using Application.Features.GithubProfiles.Commands.DeleteGithubProfile;
using Application.Features.GithubProfiles.Commands.UpdateGithubProfile;
using Application.Features.GithubProfiles.Models;
using Application.Features.GithubProfiles.Queries.GetListGithubAccount;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GithubProfileController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetGithubProfileList([FromQuery] PageRequest pageRequest)
        {
            GetListGithubProfileQuery getListGithubProfileQuery = new() { PageRequest = pageRequest };
            GithubProfileListModel result = await Mediator.Send(getListGithubProfileQuery);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGithubProfileCommand createGithubProfileCommand)
        {
            CreatedGithubProfileDto createdGithubProfileDto = await Mediator.Send(createGithubProfileCommand);
            return Created("", createdGithubProfileDto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteGithubProfileCommand deleteGithubProfileCommand)
        {
            DeletedGithubProfileDto deletedGithubProfileDto = await Mediator.Send(deleteGithubProfileCommand);
            return Ok(deletedGithubProfileDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGithubProfileCommand updateGithubProfileCommand)
        {
            UpdatedGithubProfileDto updatedGithubProfileDto = await Mediator.Send(updateGithubProfileCommand);
            return Ok(updatedGithubProfileDto);
        }
    }
}
