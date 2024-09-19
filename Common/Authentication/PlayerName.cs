using Steamworks;
using Terraria.ModLoader.IO;

namespace Universium.Common.Authentication;

[Autoload(Side = ModSide.Client)]
public sealed class PlayerName : ModPlayer
{
    private sealed class PlayerNameChangesSystemImpl : ModSystem
    {
        public override void PreSaveAndQuit() {
            base.PreSaveAndQuit();

            var player = Main.CurrentPlayer;
            
            if (!player.TryGetModPlayer(out PlayerName modPlayer)) {
                return;
            }

            player.name = modPlayer.OldName;
        }
    }

    public string OldName { get; private set; }
    
    public override void Initialize() {
        base.Initialize();

        OldName = Player.name;
    }

    public override void OnEnterWorld() {
        base.OnEnterWorld();

        Player.name = $"{SteamFriends.GetPersonaName()} | {OldName}";
    }
    
    public override void SaveData(TagCompound tag) {
        base.SaveData(tag);

        tag["oldName"] = OldName;
    }

    public override void LoadData(TagCompound tag) {
        base.LoadData(tag);

        OldName = tag.GetString("OldName");
    }
}
