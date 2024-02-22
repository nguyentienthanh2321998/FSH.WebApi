using FSH.WebApi.Application.Catalog.Brands;
using FSH.WebApi.Domain.Catalog;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class BrandsController : VersionedApiController
{


    [HttpGet("{id:int}")]
    [OpenApiOperation("Get brand details.", "")]
    public async Task<Brand> GetAsync(int id)
    {
        return await Mediator.Send(new GetBrandRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Update, FSHResource.Brands)]
    [OpenApiOperation("Create a new brand.", "")]
    public Task<int> CreateAsync(CreateBrandRequest request)
    {
        return Mediator.Send(request);
    }

}