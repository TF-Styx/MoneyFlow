using Microsoft.Extensions.DependencyInjection;
using MoneyFlow.WPF.Interfaces;
using MoneyFlow.WPF.ViewModels.WindowViewModels;
using MoneyFlow.WPF.Views.Windows;
using System.Windows;

namespace MoneyFlow.WPF.WindowFactories
{
    internal class MainWindowFactory : IWindowFactory
    {
        private readonly Lazy<IServiceProvider> _serviceProvider;

        public MainWindowFactory(Lazy<IServiceProvider> serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Window CreateWindow(object parameter = null)
        {
            var viewModel = _serviceProvider.Value.GetRequiredService<MainWindowVM>();
            viewModel.Update(parameter);

            return new MainWindow()
            {
                DataContext = viewModel,
            };
        }
    }
}
