using MoneyFlow.WPF.Enums;
using MoneyFlow.WPF.Interfaces;
using System.Windows;

namespace MoneyFlow.WPF.Services
{
    internal class NavigationWindows : INavigationWindows
    {
        private Dictionary<TypeWindow, Window> _windows = [];
        private readonly Dictionary<string, IWindowFactory> _windowFactories = [];

        public NavigationWindows(IEnumerable<IWindowFactory> windowFactories)
        {
            _windowFactories = windowFactories.ToDictionary(f => f.GetType().Name.Replace("Factory", ""), f => f);
        }

        public void OpenWindow(TypeWindow nameWindow, object parameter = null, TypeParameter typeParameter = TypeParameter.None)
        {
            if (_windows.TryGetValue(nameWindow, out var windowExist))
            {
                if (windowExist.DataContext is IUpdatable viewModel)
                {
                    viewModel.Update(parameter, typeParameter);
                }

                windowExist.Activate();
                return;
            }

            Open(nameWindow, parameter, typeParameter);
        }

        public void TransitObject(TypeWindow nameWindow, object parameter, TypeParameter typeParameter = TypeParameter.None)
        {
            throw new NotImplementedException();
        }

        private void Open(TypeWindow nameWindow, object parameter = null, TypeParameter typeParameter = TypeParameter.None)
        {
            if (_windowFactories.TryGetValue(nameWindow.ToString(), out var factory))
            {
                var window = factory.CreateWindow(parameter);

                _windows[nameWindow] = window;

                window.Closed += (c, e) => _windows.Remove(nameWindow);
                window.Show();
            }
            else
            {
                throw new Exception($"Такое окно не зарегистрировано {nameWindow}");
            }
        }

        public void CloseWindow(TypeWindow nameWindow)
        {
            if (_windows.TryGetValue(nameWindow, out var window))
            {
                window.Close();
            }
        }
    }
}
