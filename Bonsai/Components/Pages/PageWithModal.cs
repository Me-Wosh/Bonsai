using Bonsai.CustomEvents;
using Microsoft.AspNetCore.Components;

namespace Bonsai.Components.Pages;

public class PageWithModal : ComponentBase
{
    protected bool HideModal(AnimationEndedEventArgs eventArgs, ref bool modalFlag)
    {
        if (eventArgs.AnimationName != "fadeout")
        {
            return false;
        }

        modalFlag = false;
        return true;
    }
}