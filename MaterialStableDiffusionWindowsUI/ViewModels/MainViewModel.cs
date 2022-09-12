using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialStableDiffusionWindowsUI.Core.Models;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using SixLabors.ImageSharp;

namespace MaterialStableDiffusionWindowsUI.ViewModels;

public partial class MainViewModel : ObservableRecipient
{
    [ObservableProperty]
    private ObservableCollection<string> _generatedImages;

    [ObservableProperty]
    private string _prompt;
    [ObservableProperty]
    private int _width;
    [ObservableProperty]
    private int _height;
    [ObservableProperty]
    private float _scale;
    [ObservableProperty]
    private int _numOutputs;

    private GeneratorClient Generator
    {
        get;
    }

    public MainViewModel()
    {
        Generator = new();
        _generatedImages = new();

        _prompt = "";
    }

    [RelayCommand]
    private async Task Submit()
    {
        var images = await Generator.Generate(new()
        {
            Prompt = Prompt,
            Width = Width,
            Height = Height,
            GuidanceScale = Scale,
            NumOutputs = NumOutputs
        });

        var saveDir = "F:\\Generated\\Textures";
        var now = DateTime.Now;
        foreach (var (image, savePath) in images.Select((img, i) => (img, Path.Combine(saveDir, $"{now:yyyy-MM-dd_HH-mm-ss}_{i:000}.png"))))
        {
            image.SaveAsPng(savePath);
        }
    }
}
