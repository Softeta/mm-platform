using Domain.Seedwork.Enums;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Customization.TableStorage.Models;

namespace Persistence.Customization.Queries
{
    public static class QueriesRegistration
    {
        public static IServiceCollection AddSharedQueries(this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IRequestHandler<GetFileQuery, (string? FileUrl, string? FileName)>), typeof(GetFileQueryHandler))
                .AddScoped(typeof(IRequestHandler<GetImageQuery, Dictionary<ImageType, string?>?>), typeof(GetImageQueryHandler))
                .AddScoped(typeof(IRequestHandler<GetExpiredFileCachesQuery, List<FileCacheEntity>>), typeof(GetExpiredFileCachesQueryHandler));

            return services;
        }
    }
}
