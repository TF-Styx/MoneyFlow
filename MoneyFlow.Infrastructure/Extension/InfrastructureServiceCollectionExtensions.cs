using Microsoft.Extensions.DependencyInjection;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.Repositories;

namespace MoneyFlow.Infrastructure.Extension
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<ContextMF>();
            services.AddSingleton<Func<ContextMF>>(provider => () => provider.GetRequiredService<ContextMF>());


            services.AddScoped<IBanksRepository, BanksRepository>();
            services.AddScoped<IGendersRepository, GendersRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();

            return services;
        }
    }
}
