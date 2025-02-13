using Microsoft.Extensions.DependencyInjection;
using MoneyFlow.Application.Extension;
using MoneyFlow.Infrastructure.Extension;
using MoneyFlow.WPF.Enums;
using MoneyFlow.WPF.Interfaces;
using MoneyFlow.WPF.Services;
using MoneyFlow.WPF.ViewModels.PageViewModels;
using MoneyFlow.WPF.ViewModels.WindowViewModels;
using MoneyFlow.WPF.Views.Pages;
using MoneyFlow.WPF.Views.Windows;
using MoneyFlow.WPF.WindowFactories;
using System.Windows;

namespace MoneyFlow.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        // Свойство хранения сервисов
        public IServiceProvider ServiceProvider { get; private set; }

        // Перегруженные метод запуска приложения
        protected override async void OnStartup(StartupEventArgs e)
        {
            // Создали коллекцию сервисов
            var services = new ServiceCollection();

            // Добавили в коллекцию
            services.AddInfrastructureServices();
            services.AddApplicationServices();

            ConfigureServices(services);
            ConfigureWindow(services);
            ConfigurePage(services);

            // Сконфигурировали ServiceProvider
            ServiceProvider = services.BuildServiceProvider();

            var navigationWindows = ServiceProvider.GetService<INavigationWindows>();
            navigationWindows.OpenWindow(TypeWindow.AuthWindow);

            // Остальная реализация взята из оригинального класса
            base.OnStartup(e);
        }

        // Добавляем окна и их VM в коллекцию сервисов 
        private void ConfigureWindow(ServiceCollection services)
        {
            services.AddTransient<IWindowFactory, AuthWindowFactory>();
            services.AddTransient<IWindowFactory, MainWindowFactory>();
            services.AddTransient<IWindowFactory, AddBaseInformationWindowFactory>();

            services.AddTransient(typeof(Lazy<>), typeof(LazyService<>));

            services.AddTransient<AuthWindowVM>();
            services.AddTransient<MainWindowVM>();
            services.AddTransient<AddBaseInformationVM>();
        }

        // Добавляем страницы и их VM в коллекцию сервисов 
        private void ConfigurePage(ServiceCollection services)
        {
            services.AddSingleton<BankPage>();
            services.AddSingleton<BankPageVM>();

            services.AddSingleton<GenderPage>();
            services.AddSingleton<GenderPageVM>();
        }

        // Добавляем страницы и их VM в коллекцию сервисов 
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<INavigationPages, NavigationPages>();
            services.AddSingleton<INavigationWindows, NavigationWindows>();
        }
    }
}
