using Microsoft.AspNetCore.Identity;

namespace FSH.WebApi.Infrastructure.Identity;

public class ApplicationRoleClaim : IdentityRoleClaim<int>
{
    public string? CreatedBy { get; init; }
    public DateTime CreatedOn { get; init; }
}