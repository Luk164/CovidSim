namespace CovidSimGUI.Contracts.Views
{
    public interface INavigationAware
    {
        void OnNavigatedTo(object parameter);

        void OnNavigatedFrom();
    }
}
