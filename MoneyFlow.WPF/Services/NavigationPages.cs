using MoneyFlow.WPF.Enums;
using MoneyFlow.WPF.Interfaces;
using System.Windows.Controls;

namespace MoneyFlow.WPF.Services
{
    internal class NavigationPages : INavigationPages
    {
        private Dictionary<PageType, Page> _page = [];
        private readonly Dictionary<string, IPageFactory> _pageFactories = [];
        private Frame _frame;

        public NavigationPages(IEnumerable<IPageFactory> pageFactories)
        {
            _pageFactories = pageFactories.ToDictionary(f => f.GetType().Name.Replace("Factory", ""), f => f);
        }

        public void OpenPage(PageType pageName, object parameter = null, ParameterType parameterType = ParameterType.None)
        {
            if (_page.TryGetValue(pageName, out var pageExist))
            {
                if (pageExist.DataContext is IUpdatable viewModel)
                {
                    viewModel.Update(parameter, parameterType);
                }

                _frame.Navigate(pageExist);
                return;
            }

            Open(pageName, parameter, parameterType);
        }

        public void TransitObject(PageType pageName, object parameter = null, ParameterType parameterType = ParameterType.None)
        {
            if (_page.TryGetValue(pageName, out var pageExist))
            {
                if (pageExist.DataContext is IUpdatable viewModel)
                {
                    viewModel.Update(parameter, parameterType);
                }
            }
        }

        private void Open(PageType pageName, object parameter = null, ParameterType parameterType = ParameterType.None)
        {
            if (_pageFactories.TryGetValue(pageName.ToString(), out var factory))
            {
                var page = factory.CreatePage(parameter);

                _page.Add(pageName, page);

                _frame.Navigate(page);
            }
            else
            {
                throw new Exception($"Страничка с таким именем «{pageName}» не зарегистрирована !!");
            }
        }

        public void SetFrame(Frame frame)
        {
            _frame = frame;
        }
    }
}
