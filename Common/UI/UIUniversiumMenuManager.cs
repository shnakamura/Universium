using Universium.Core.UI;

namespace Universium.Common.UI;

[Autoload(Side = ModSide.Client)]
public sealed class UIUniversiumMenuManager : ModSystem
{
    public override void OnWorldLoad() {
        base.OnWorldLoad();

        UIManager.TryEnableOrRegister(UIUniversiumMenu.Identifier, "Vanilla: Mouse Text", new UIUniversiumMenu());
    }

    public override void OnWorldUnload() {
        base.OnWorldUnload();

        UIManager.TryDisable(UIUniversiumMenu.Identifier);
    }
}
