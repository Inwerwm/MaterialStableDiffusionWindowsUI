using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialStableDiffusionWindowsUI.Core.Models;
using MaterialStableDiffusionWindowsUI.Helpers;
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

    [ObservableProperty]
    private bool _isProgress;
    [ObservableProperty]
    private string _progressMessage;

    [ObservableProperty]
    private int _imageViewSize;

    private GeneratorClient Generator
    {
        get;
    }

    public MainViewModel()
    {
        Generator = new();
        _generatedImages = new();

        _prompt = "";
        _width = 512;
        _height = 512;
        _scale = 7.5f;
        _numOutputs = 1;

        _progressMessage = "";

        _imageViewSize = 400;
    }

    [RelayCommand]
    private async Task Submit()
    {
        IsProgress = true;
        ProgressMessage = "ProgressString_GeneratingImages".GetLocalized();

        try
        {
            var images = await Generator.Generate(new()
            {
                Prompt = Prompt,
                Width = Width,
                Height = Height,
                GuidanceScale = Scale,
                NumOutputs = NumOutputs
            });

            if(images is null) { return; }

            ProgressMessage = "ProgressString_SavingImages".GetLocalized();
            var saveDir = "F:\\Generated\\Textures";
            var now = DateTime.Now;
            foreach (var (image, savePath) in images.Select((img, i) => (img, Path.Combine(saveDir, $"{now:yyyy-MM-dd_HH-mm-ss}_{i:000}.png"))))
            {
                await image.SaveAsPngAsync(savePath);
                GeneratedImages.Add(savePath);
            }
        }
        finally
        {
            ProgressMessage = "";
            IsProgress = false;
        }
    }
}
