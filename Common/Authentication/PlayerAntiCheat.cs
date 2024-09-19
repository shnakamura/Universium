using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Universium.Common.Authentication;

[Autoload(Side = ModSide.Client)]
public sealed class PlayerAntiCheat : ModSystem
{
    public static readonly string EnabledPath = Path.Combine(Main.SavePath, "Mods", "enabled.json");
    
    public static readonly IReadOnlyList<string> Whitelist = [
        "RecipeBrowser",
        "Census",
        "HEROsMod",
        "BossCursor",
        "BossChecklist"
    ];
    
    public override void PostSetupContent() {
        base.PostSetupContent();

        var text = File.ReadAllText(EnabledPath);
        var mods = JsonConvert.DeserializeObject<string[]?>(text);

        if (mods == null) {
            throw new Exception();
        }

        var blacklisted = mods.Any(mod => !Whitelist.Contains(mod));

        if (!blacklisted) {
            return;
        }

    }
}
