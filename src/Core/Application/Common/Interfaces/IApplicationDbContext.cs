using Microsoft.EntityFrameworkCore;
namespace FSH.WebApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Brand> Brands { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellation);
}