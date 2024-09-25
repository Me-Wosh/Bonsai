function matchWidth(identifier)
{
    const elements = document.querySelectorAll(identifier);

    if (!elements || elements.length == 0)
    {
        return;
    }

    let largest = elements[0];

    elements.forEach(element =>
    {
        if (element.getBoundingClientRect().width > largest.getBoundingClientRect().width)
        {
            largest = element;
        }
    });

    // get values without the "px" suffix
    let paddingLeft  = window.getComputedStyle(largest).getPropertyValue("padding-left").slice(0, -2);
    let paddingRight = window.getComputedStyle(largest).getPropertyValue("padding-right").slice(0, -2);
    let borderLeft   = window.getComputedStyle(largest).getPropertyValue("border-left-width").slice(0, -2);
    let borderRight  = window.getComputedStyle(largest).getPropertyValue("border-right-width").slice(0, -2);

    elements.forEach(element =>
    {
        element.style.width = largest.getBoundingClientRect().width - paddingLeft - paddingRight - borderLeft - borderRight + "px";
    });
}