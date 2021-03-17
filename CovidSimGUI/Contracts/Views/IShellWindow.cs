using System.Windows.Controls;

namespace CovidSimGUI.Contracts.Views
{
    public interface IShellWindow
    {
        Frame GetNavigationFrame();

        void ShowWindow();

        void CloseWindow();
    }
}
