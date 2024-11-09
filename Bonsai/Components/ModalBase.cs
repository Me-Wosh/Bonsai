using Bonsai.CustomEvents;
using Microsoft.AspNetCore.Components;

namespace Bonsai.Components;

public class ModalBase : ComponentBase
{
    [Parameter]
    public string? HeaderText { get; set; }

    [Parameter]
    public bool HideCancelButton { get; set; }

    [Parameter]
    public string FooterButtonText { get; set; } = "OK";

    [Parameter]
    public EventCallback<AnimationEndedEventArgs> OnAnimationEndedCallback { get; set; }
}