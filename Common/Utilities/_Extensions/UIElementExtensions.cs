using Terraria.UI;

namespace Universium.Common.Utilities;

/// <summary>
///     Basic <see cref="UIElement"/> extensions.
/// </summary>
public static class UIElementExtensions
{
    public static void Toggle(this UIElement? element, UIElement? value) {
        if (element.HasChild(value)) {
            element.RemoveChild(value);
        }
        else {
            element.Append(value);
        }
    }
}
