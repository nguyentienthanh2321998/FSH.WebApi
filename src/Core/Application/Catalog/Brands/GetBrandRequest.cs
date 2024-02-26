

using Microsoft.EntityFrameworkCore;

namespace FSH.WebApi.Application.Catalog.Brands;

public class GetBrandRequest : IRequest<Brand>
{
    public int Id { get; set; }

    public GetBrandRequest(int id) => Id = id;
}


public class GetBrandRequestHandler : IRequestHandler<GetBrandRequest, Brand>
{
    private readonly IApplicationDbContext _context;

    public GetBrandRequestHandler(IApplicationDbContext context) => (_context) = (context);

    public async Task<Brand> Handle(GetBrandRequest request, CancellationToken cancellationToken)
    {
        var brand = await _context.Brands.Where(x => x.Id == (request.Id)).FirstOrDefaultAsync(cancellationToken);
     return brand   ?? throw new NotFoundException(string.Format("brand.notfound", request.Id));
    }
        
}