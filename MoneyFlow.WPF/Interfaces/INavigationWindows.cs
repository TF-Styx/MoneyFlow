﻿using MoneyFlow.WPF.Enums;

namespace MoneyFlow.WPF.Interfaces
{
    internal interface INavigationWindows
    {
        void OpenWindow(TypeWindow nameWindow, object parameter = null, TypeParameter typeParameter = TypeParameter.None);

        void TransitObject(TypeWindow nameWindow, object parameter, TypeParameter typeParameter = TypeParameter.None);

        void CloseWindow(TypeWindow nameWindow);

        void MinimizeWindow(TypeWindow nameWindow);

        void MaximizeWindow(TypeWindow nameWindow);

        void RestoreWindow(TypeWindow nameWindow);

        void Shutdown();
    }
}
