using MaterialStableDiffusionWindowsUI.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace MaterialStableDiffusionWindowsUI.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
