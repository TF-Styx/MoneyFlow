using MoneyFlow.WPF.Enums;

namespace MoneyFlow.WPF.Interfaces
{
    internal interface IUpdatable
    {
        void Update(object parameter, TypeParameter typeParameter = TypeParameter.None);
    }
}
