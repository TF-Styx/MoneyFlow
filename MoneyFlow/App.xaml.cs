using Microsoft.Extensions.DependencyInjection;
using MoneyFlow.MVVM.Models.MSSQL_DB;
using MoneyFlow.Utils.Services.AuthorizationVerificationServices;
using MoneyFlow.Utils.Services.DataBaseServices;
using MoneyFlow.Utils.Services.NavigationServices.PageNavigationsService;
using MoneyFlow.Utils.Services.NavigationServices.WindowNavigationsService;
using System.Windows;

namespace MoneyFlow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Свойство хранения сервисов
        public IServiceProvider ServiceProvider { get; private set; }

        // Перегруженные метод запуска приложения
        protected override async void OnStartup(StartupEventArgs e)
        {
            // Создали коллекцию сервисов
            var services = new ServiceCollection();
            // Добавили сервисы в коллекцию
            ConfigureServices(services);
            // Сконфигурировали ServiceProvider
            ServiceProvider = services.BuildServiceProvider();

            var authorizationService = ServiceProvider.GetService<IAuthorizationVerificationService>();
            var dataBaseService = ServiceProvider.GetService<IDataBaseService>();
            var windowNavigationService = ServiceProvider.GetService<IWindowNavigationService>();

            if (authorizationService.CheckAuthorization())
            {
                if (await dataBaseService.ExistsAsync<User>(x => x.Login.ToLower() == authorizationService.CurrentUser.Login.ToLower()))
                {
                    windowNavigationService.NavigateTo("MainWindow", authorizationService.CurrentUser);
                }
                else
                {
                    windowNavigationService.NavigateTo("AuthWnd");
                }
            }
            else
            {
                windowNavigationService.NavigateTo("AuthWnd");
            }

            // Остальная реализация взята из оригинального класса
            base.OnStartup(e);
        }
        
        // Добавляем сервисы в коллекцию сервисов 
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IWindowNavigationService, WindowNavigationService>();
            services.AddSingleton<IPageNavigationService, PageNavigationService>();
            services.AddSingleton<IAuthorizationVerificationService, AuthorizationVerificationService>();

            services.AddTransient<MoneyFlowContext>();
            services.AddSingleton<Func<MoneyFlowContext>>(provider => () => provider.GetRequiredService<MoneyFlowContext>());
            services.AddSingleton<IDataBaseService, DataBaseService>();
        }
    }
}
