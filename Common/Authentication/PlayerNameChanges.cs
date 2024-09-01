using Steamworks;
using Terraria.ModLoader.IO;

namespace Universium.Common.Authentication;

[Autoload(Side = ModSide.Client)]
public sealed class PlayerNameChanges : ModPlayer
{
    private sealed class PlayerNameChangesSystemImpl : ModSystem
    {
        private static Player Player => Main.CurrentPlayer;
        
        public override void PreSaveAndQuit() {
            base.PreSaveAndQuit();

            if (!Player.TryGetModPlayer(out PlayerNameChanges modPlayer)) {
                throw new Exception("Could not retrieve the ModPlayer instance of PlayerNameChanges.");
            }

            Player.name = modPlayer.OldName;
        }
    }
    
    /// <summary>
    ///     The original name of this player.
    /// </summary>
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
