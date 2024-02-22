
using FSH.WebApi.Application.Common.Caching;

namespace FSH.WebApi.Infrastructure.Caching;

public class CacheKeyService : ICacheKeyService
{


    public string GetCacheKey(string name, object id, bool includeTenantId = true)
    {
        string tenantId =  "GLOBAL";
        return $"{tenantId}-{name}-{id}";
    }
}