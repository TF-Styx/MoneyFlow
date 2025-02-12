using Microsoft.Extensions.DependencyInjection;
using MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces;
using MoneyFlow.Application.UseCases.BankCases;

namespace MoneyFlow.Application.Extension
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICreateBankUseCase, CreateBankUseCase>();
            services.AddScoped<IDeleteBankUseCase, DeleteBankUseCase>();
            services.AddScoped<IGetBankUseCase,    GetBankUseCase>();
            services.AddScoped<IUpdateBankUseCase, UpdateBankUseCase>();

            // TODO : Сделать тоже самое для гендеров
            //services.AddScoped<ICreateBankUseCase, CreateBankUseCase>();
            //services.AddScoped<IDeleteBankUseCase, DeleteBankUseCase>();
            //services.AddScoped<IGetBankUseCase,    GetBankUseCase>();
            //services.AddScoped<IUpdateBankUseCase, UpdateBankUseCase>();
            
            return services;
        }
    }
}
