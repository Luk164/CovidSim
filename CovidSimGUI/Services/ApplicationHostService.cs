using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CovidSimGUI.Contracts.Services;
using CovidSimGUI.Contracts.Views;
using CovidSimGUI.Views;

using Microsoft.Extensions.Hosting;

namespace CovidSimGUI.Services
{
    public class ApplicationHostService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigationService _navigationService;
        private readonly IThemeSelectorService _themeSelectorService;
        private IShellWindow _shellWindow;

        public ApplicationHostService(IServiceProvider serviceProvider, INavigationService navigationService, IThemeSelectorService themeSelectorService)
        {
            _serviceProvider = serviceProvider;
            _navigationService = navigationService;
            _themeSelectorService = themeSelectorService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Initialize services that you need before app activation
            await InitializeAsync();

            await HandleActivationAsync();

            // Tasks after activation
            await StartupAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private Task InitializeAsync()
        {
            _themeSelectorService.InitializeTheme();
            return Task.CompletedTask;
        }

        private Task StartupAsync()
        {
            return Task.CompletedTask;
        }

        private async Task HandleActivationAsync()
        {
            if (!Application.Current.Windows.OfType<IShellWindow>().Any())
            {
                // Default activation that navigates to the apps default page
                _shellWindow = _serviceProvider.GetService(typeof(IShellWindow)) as IShellWindow;
                if (_shellWindow != null)
                {
                    _navigationService.Initialize(_shellWindow.GetNavigationFrame());
                    _shellWindow.ShowWindow();
                }

                _navigationService.NavigateTo(typeof(SimulatorPage));
                await Task.CompletedTask;
            }
        }
    }
}
