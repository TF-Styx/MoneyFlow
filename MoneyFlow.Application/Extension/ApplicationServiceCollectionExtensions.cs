using Microsoft.Extensions.DependencyInjection;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.Application.Services.Realization;
using MoneyFlow.Application.UseCaseInterfaces.BankCaseInterfaces;
using MoneyFlow.Application.UseCaseInterfaces.GenderCaseInterfaces;
using MoneyFlow.Application.UseCaseInterfaces.UserCaseInterfaces;
using MoneyFlow.Application.UseCases.BankCases;
using MoneyFlow.Application.UseCases.GenderCases;
using MoneyFlow.Application.UseCases.UserCases;

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

            services.AddScoped<ICreateGenderUseCase, CreateGenderUseCase>();
            services.AddScoped<IDeleteGenderUseCase, DeleteGenderUseCase>();
            services.AddScoped<IGetGenderUseCase,    GetGenderUseCase>();
            services.AddScoped<IUpdateGenderUseCase, UpdateGenderUseCase>();

            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
            services.AddScoped<IGetUserUseCase,    GetUserUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();

            services.AddSingleton<IAuthorizationService, AuthorizationService>();
            services.AddSingleton<IRegistrationService, RegistrationService>();
            services.AddSingleton<IRecoveryService, RecoveryService>();
            services.AddSingleton<IBankService,   BankService>();
            services.AddSingleton<IGenderService, GenderService>();
            services.AddSingleton<IUserService,   UserService>();

            return services;
        }
    }
}
