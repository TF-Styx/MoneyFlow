using Microsoft.Extensions.DependencyInjection;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.Application.Services.Realization;
using MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces;
using MoneyFlow.Application.UseCaseInterfaces.GenderCaseInterfaces;
using MoneyFlow.Application.UseCases.BankCases;
using MoneyFlow.Application.UseCases.GenderCases;

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

            // TODO : Сделать тоже самое для гендеров : СДЕЛАНО
            services.AddScoped<ICreateGenderUseCase, CreateGenderUseCase>();
            services.AddScoped<IDeleteGenderUseCase, DeleteGenderUseCase>();
            services.AddScoped<IGetGenderUseCase,    GetGenderUseCase>();
            services.AddScoped<IUpdateGenderUseCase, UpdateGenderUseCase>();

            services.AddSingleton<IBankService, BankService>();
            services.AddSingleton<IGenderService, GenderService>();

            return services;
        }
    }
}
