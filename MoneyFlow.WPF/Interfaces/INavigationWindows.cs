using MoneyFlow.WPF.Enums;

namespace MoneyFlow.WPF.Interfaces
{
    internal interface INavigationWindows
    {
        void OpenWindow(string nameWindow, object parameter = null, TypeParameter typeParameter = TypeParameter.None);

        void TransitObject(string nameWindow, object parameter, TypeParameter typeParameter = TypeParameter.None);
    }
}
