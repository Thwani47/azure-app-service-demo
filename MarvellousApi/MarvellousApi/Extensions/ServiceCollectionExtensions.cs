using MarvellousApi.Options;
using MarvellousApi.Services;

namespace MarvellousApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMarvellousApiExtensions(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        
        serviceCollection.Configure<MarvelOptions>(configuration.GetSection("MarvelApiOptions"));
        serviceCollection.AddSingleton<IMarvelService, MarvelService>();
        return serviceCollection;
    }
}