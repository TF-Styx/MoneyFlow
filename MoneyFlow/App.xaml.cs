using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using MoneyFlow.MVVM.View.Windows;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            // Создали коллекцию сервисов
            var services = new ServiceCollection();
            // Добавили сервисы в коллекцию
            ConfigureServices(services);
            // Сконфигурировали ServiceProvider
            ServiceProvider = services.BuildServiceProvider();

            var windowNavigationService = ServiceProvider.GetService<IWindowNavigationService>();
            windowNavigationService.NavigateTo("AuthWnd");

            // Остальная реализация взята из оригинального класса
            base.OnStartup(e);
        }
        
        // Добавляем сервисы в коллекцию сервисов 
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IWindowNavigationService, WindowNavigationService>();
            services.AddSingleton<IPageNavigationService, PageNavigationService>();
        }
    }
}
