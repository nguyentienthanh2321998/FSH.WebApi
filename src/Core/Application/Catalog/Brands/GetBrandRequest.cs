

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
    private readonly IStringLocalizer<GetBrandRequestHandler> _localizer;

    public GetBrandRequestHandler(IApplicationDbContext context, IStringLocalizer<GetBrandRequestHandler> localizer) => (_context, _localizer) = (context, localizer);

    public async Task<Brand> Handle(GetBrandRequest request, CancellationToken cancellationToken)
    {
        var brand = await _context.Brands.Where(x => x.Id == (request.Id)).FirstOrDefaultAsync(cancellationToken);
     return brand   ?? throw new NotFoundException(string.Format(_localizer["brand.notfound"], request.Id));
    }
        
}