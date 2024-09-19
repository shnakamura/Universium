using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Universium.Common.Authentication;

[Autoload(Side = ModSide.Client)]
public sealed class PlayerAntiCheatSystem : ModSystem
{
    public static readonly IReadOnlyList<string> Whitelist = [
        "RecipeBrowser",
        "Census",
        "HEROsMod",
        "BossCursor",
        "BossChecklist"
    ];
    
    public override void PostSetupContent() {
        base.PostSetupContent();

        var path = Path.Combine(Main.SavePath, "Mods", "enabled.json");

        if (!File.Exists(path)) {
            throw new Exception("Could not find the file containing the list of enabled mods.");
        }

        var text = File.ReadAllText(path);
        var mods = JsonConvert.DeserializeObject<string[]?>(text);

        if (mods == null) {
            throw new Exception("Could not retrieve the list of enabled mods.");
        }

        for (int i = 0; i < mods.Length; i++) {
            var whitelisted = false;
            
            for (int j = 0; j < Whitelist.Count; j++) {
                if (mods[i] == Whitelist[j]) {
                    whitelisted = true;
                    break;
                }
            }

            if (whitelisted) {
                continue;
            }
            
            throw new Exception("A blacklisted mod was detected: " + mods[i]);
        }
    }
}
