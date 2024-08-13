export function afterStarted(blazor) {
    blazor.registerCustomEventType('animationended', {
        browserEventName: 'animationend',
        createEventArgs: event => {
            return {
                // all html and css properties have unique identifier and .split() is getting rid of it since im only
                // interested in the name and not the identifier
                // example: slideout-b-5ch2bagayu
                animationName: event.animationName.split('-')[0]
            };
        }
    });
}