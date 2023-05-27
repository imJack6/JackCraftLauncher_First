using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Styling;

namespace JackCraftLauncher.Desktop.Views.MyControls;

[PseudoClasses(":normal")]
public class AnimateTabControl : TabControl, IStyleable
{
    public static readonly StyledProperty<bool> AnimateOnChangeProperty =
        AvaloniaProperty.Register<AnimateTabControl, bool>(nameof(AnimateOnChange), true);

    public AnimateTabControl()
    {
        PseudoClasses.Add(":normal");
        this.GetObservable(SelectedContentProperty).Subscribe(OnContentChanged);
        //this.GetObservable(SelectedContentProperty).Subscribe(Observer.Create<object>(OnContentChanged));
    }

    public bool AnimateOnChange
    {
        get => GetValue(AnimateOnChangeProperty);
        set => SetValue(AnimateOnChangeProperty, value);
    }

    Type IStyleable.StyleKey => typeof(TabControl);

    private void OnContentChanged(object? obj)
    {
        if (AnimateOnChange)
        {
            PseudoClasses.Remove(":normal");
            PseudoClasses.Add(":normal");
        }
    }
}