using MoneyFlow.WPF.Enums;
using System.Windows.Controls;

namespace MoneyFlow.WPF.Interfaces
{
    internal interface INavigationPages
    {
        void OpenPage(PageType namePage, object parameter = null, ParameterType parameterType = ParameterType.None);
        void TransitObject(PageType pageName, object parameter = null, ParameterType parameterType = ParameterType.None);
        void SetFrame(Frame frame);
    }
}
