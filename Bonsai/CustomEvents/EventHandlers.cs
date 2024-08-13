using Microsoft.AspNetCore.Components;

namespace Bonsai.CustomEvents;

[EventHandler("onanimationended", typeof(AnimationEndedEventArgs), enableStopPropagation: true, enablePreventDefault: true)]
public static class EventHandlers
{
}