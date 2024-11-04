using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blueshift.Sample.Adapters.Repositories.SqlServer.Entities;
using Blueshift.Sample.Ports.Repositories;
using Blueshift.Sample.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blueshift.Sample.Adapters.Repositories.SqlServer;

/// <inheritdoc />
/// <param name="sampleContext">An instance of a <see cref="SampleContext"/> for working with <see cref="SqlMember"/> records.</param>
/// <param name="mapper">An instance of <see cref="IMapper"/> for mapping between core entities and their database records.</param>
public class MemberRepository(SampleContext sampleContext, IMapper mapper) : IMemberRepository
{
    private readonly SampleContext _sampleContext = sampleContext ?? throw new ArgumentNullException(nameof(sampleContext));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    /// <inheritdoc />
    public async Task<Member> CreateMemberAsync(Member member)
    {
        var sqlMember = mapper.Map<SqlMember>(member);

        try
        {
            await _sampleContext.Members.AddAsync(sqlMember);
            await _sampleContext.SaveChangesAsync();
        }
        catch
        {
            // TODO: check exception here
            throw;
        }

        return _mapper.Map<Member>(sqlMember);
    }

    /// <inheritdoc />
    public async Task<Member?> GetMemberByIdAsync(Guid memberId)
    {
        Member? member = null;

        try
        {
            SqlMember? sqlMember = await _sampleContext.Members.FindAsync(memberId);
            if (sqlMember is not null)
            {
                member = _mapper.Map<Member>(sqlMember);
            }
        }
        catch
        {
            // TODO: check exception here
            throw;
        }

        return member;
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<Member>> GetMembersAsync()
    {
        IReadOnlyCollection<Member> members = _mapper.ProjectTo<Member>(_sampleContext.Members)
            .ToList()
            .AsReadOnly();

        return Task.FromResult(members);
    }

    /// <inheritdoc />
    public async Task<Member?> UpdateMemberAsync(Member member)
    {
        var sqlMember = mapper.Map<SqlMember>(member);

        try
        {
            _sampleContext.Members.Update(sqlMember);
            await _sampleContext.SaveChangesAsync();
        }
        catch
        {
            // TODO: check exception here
            throw;
        }

        return _mapper.Map<Member>(sqlMember);
    }

    /// <inheritdoc />
    public async Task DeleteMemberAsync(Guid memberId)
    {
        try
        {
            int count = await _sampleContext.Members
                .Where(member => member.MemberId == memberId)
                .ExecuteDeleteAsync();

            // TODO: check deleted count
        }
        catch
        {
            // TODO: check exception here
            throw;
        }
    }
}
