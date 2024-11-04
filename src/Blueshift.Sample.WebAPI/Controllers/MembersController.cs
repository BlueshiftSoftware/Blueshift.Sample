using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Blueshift.Sample.Entities;
using Blueshift.Sample.Ports.Repositories;
using Blueshift.Sample.WebAPI.Models;
using Blueshift.Sample.WebAPI.Models.Members;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blueshift.Sample.WebAPI.Controllers;

/// <summary>
/// WebAPI controller for Member models.
/// </summary>
/// <param name="mapper">An instance of <see cref="IMapper"/> for mapping between business entities and their models.</param>
/// <param name="logger">An instance of <see cref="ILogger{T}"/> for logging the controller's activity.</param>
/// <param name="memberRepository">An instance of <see cref="IMemberRepository"/> to use for retrieving and updating <see cref="Member"/> data.</param>
[ApiController]
[Route("[controller]")]
public class MembersController(
    ILogger<MembersController> logger,
    IMapper mapper,
    IMemberRepository memberRepository) : ControllerBase
{
    private readonly ILogger<MembersController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IMemberRepository _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));

    /// <summary>
    /// Gets a list of available Members.
    /// </summary>
    /// <returns>A collection of <see cref="MemberModel"/> instances.</returns>
    /// <response code="200">Returns a collection of <see cref="MemberModel"/> instances.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PageModel<MemberModel>>> GetAsync(
        CancellationToken requestCancellationToken,
        [FromQuery] int? page = null,
        [FromQuery] int? pageSize = null)
    {
        using CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(requestCancellationToken);
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        IEnumerable<Member> members = await _memberRepository.GetMembersAsync();
        IEnumerable<MemberModel> memberModels = _mapper.Map<IEnumerable<MemberModel>>(members);

        var pageModel = new PageModel<MemberModel>(memberModels)
        {
            CurrentPage = page ?? -1,
            PageSize = pageSize ?? -1,
            TotalItems = 100
        };

        return Ok(pageModel);
    }

    /// <summary>
    /// Gets the model for a specific Member record.
    /// </summary>
    /// <param name="memberId">The unique identifier of the Member to fetch.</param>
    /// <returns>A single <see cref="MemberModel"/>, if found.</returns>
    /// <response code="200">Returns a single <see cref="MemberModel"/>, if found.</response>
    /// <response code="404">Returns a <see cref="NotFoundResult"/> if the requested application cannot be found.</response>
    [HttpGet("{memberId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberModel>> Get(Guid memberId)
    {
        Member? member = await _memberRepository.GetMemberByIdAsync(memberId);
        return member != null
            ? Ok(_mapper.Map<MemberModel>(member))
            : NotFound();
    }

    /// <summary>
    /// Creates a new Member record.
    /// </summary>
    /// <param name="createMemberRequest">The request model containing the values for the new Member.</param>
    /// <returns>The newly created <see cref="MemberModel"/>, if successful.</returns>
    /// <response code="201">Returns the newly created <see cref="MemberModel"/>, if successful.</response>
    /// <response code="400">Returns a <see cref="BadRequestResult"/> if the request fails.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MemberModel>> Post([FromBody] CreateMemberRequest createMemberRequest)
    {
        Member newMember = _mapper.Map<Member>(createMemberRequest);
        newMember = await _memberRepository.CreateMemberAsync(newMember);
        MemberModel memberModel = _mapper.Map<MemberModel>(newMember);
        return Created($"/members/{newMember.MemberId}", memberModel);
    }


    /// <summary>
    /// Updates a Member record.
    /// </summary>
    /// <param name="memberId">The unique identifier for the Member to update.</param>
    /// <param name="updateMemberRequest">The request model containing the new settings for the Member.</param>
    /// <returns>The updated <see cref="MemberModel"/>, if successful.</returns>
    /// <response code="200">Returns updated <see cref="MemberModel"/>, if successful.</response>
    /// <response code="400">Returns a <see cref="BadRequestResult"/> if the request fails.</response>
    /// <response code="404">Returns a <see cref="NotFoundResult"/> if application to be udpated cannot be found.</response>
    [HttpPut("{memberId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberModel>> Put(
        Guid memberId,
        [FromBody] UpdateMemberRequest updateMemberRequest)
    {
        if (memberId != updateMemberRequest.MemberId)
        {
            throw new Exception("The memberId in the request route must match the id in the POST body.");
        }

        Member? newMember = _mapper.Map<Member>(updateMemberRequest);
        newMember = await _memberRepository.UpdateMemberAsync(newMember);
        MemberModel memberModel = _mapper.Map<MemberModel>(newMember);
        return Ok(memberModel);
    }

    /// <summary>
    /// Deletes a Member.
    /// </summary>
    /// <param name="memberId">The unique identifier for the Member to delete.</param>
    /// <returns>A single <see cref="MemberModel"/>, if found.</returns>
    /// <response code="204">Returns a <see cref="NoContentResult"/> if successful.</response>
    /// <response code="404">Returns a <see cref="NotFoundResult"/> if the requested Member cannot be found.</response>
    [HttpDelete("{memberId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid memberId)
    {
        await _memberRepository.DeleteMemberAsync(memberId);
        return NoContent();
    }
}
