namespace FSH.WebApi.Application.Catalog.Brands;

public class CreateBrandRequest : IRequest<int>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}


public class CreateBrandRequestHandler : IRequestHandler<CreateBrandRequest, int>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IApplicationDbContext _context;

    public CreateBrandRequestHandler(IApplicationDbContext context) => _context = context;

    public async Task<int> Handle(CreateBrandRequest request, CancellationToken cancellationToken)
    {
        var brand = new Brand(request.Name, request.Description);

        _context.Brands.Add(brand);
        await _context.SaveChangesAsync(cancellationToken);
        return brand.Id;
    }
}