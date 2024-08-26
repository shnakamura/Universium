using System.Reflection.Metadata;
using Terraria.UI;
using Universium.Core.UI;

[assembly: MetadataUpdateHandler(typeof(UIHotReloadUpdateHandler))]

namespace Universium.Core.UI;

internal static class UIHotReloadUpdateHandler
{
    internal static void ClearCache(Type[]? types) { }

    internal static void UpdateApplication(Type[]? updatedTypes) {
        Main.QueueMainThreadAction(
            () => {
                foreach (var type in updatedTypes) {
                    if (!typeof(UIState).IsAssignableFrom(type) || !typeof(UIElement).IsAssignableFrom(type)) {
                        continue;
                    }

                    UIManager.RefreshAllStates();
                }
            }
        );
    }
}
