using System.IO;
using Newtonsoft.Json;

namespace Universium.Common.Authentication;

[Autoload(Side = ModSide.Client)]
public sealed class PlayerAntiCheatSystem : ModSystem
{
    /// <summary>
    ///     The list of names of whitelisted mods.
    /// </summary>
    public static readonly string[] Whitelist = {
        "RecipeBrowser",
        "Census",
        "HEROsMod",
        "BossCursor",
        "BossChecklist"
    };
    
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
            var isWhitelisted = false;
            
            for (int j = 0; j < Whitelist.Length; j++) {
                if (mods[i] == Whitelist[j]) {
                    isWhitelisted = true;
                    break;
                }
            }

            if (isWhitelisted) {
                continue;
            }
            
            throw new Exception("A blacklisted mod was detected: " + mods[i]);
        }
    }
}
