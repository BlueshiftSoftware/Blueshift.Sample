namespace Blueshift.Sample.WebAPI.Models.Members;

/// <summary>
/// A request to create a new Member.
/// </summary>
public class CreateMemberRequest
{
    /// <summary>
    /// The Member's given name.
    /// </summary>
    public string? GivenName { get; set; }

    /// <summary>
    /// The Member's surname.
    /// </summary>
    public string? Surname { get; set; }
}
