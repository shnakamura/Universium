using Terraria.ModLoader.IO;

namespace Universium.Common.Effects;

public sealed class PlayerHardcore : ModPlayer
{
    private sealed class PlayerHardcoreSystemImpl : ModSystem
    {
        public override void PreSaveAndQuit() {
            base.PreSaveAndQuit();

            var player = Main.CurrentPlayer;

            if (!player.TryGetModPlayer(out PlayerHardcore modPlayer)) {
                return;
            }

            player.difficulty = modPlayer.OldDifficulty;
        }
    }
    
    public byte OldDifficulty { get; private set; }
    
    public override void Initialize() {
        base.Initialize();

        OldDifficulty = Player.difficulty;
    }

    public override void OnEnterWorld() {
        base.OnEnterWorld();

        Player.difficulty = PlayerDifficultyID.Hardcore;
    }
    
    public override void SaveData(TagCompound tag) {
        base.SaveData(tag);

        tag["oldDifficulty"] = OldDifficulty;
    }

    public override void LoadData(TagCompound tag) {
        base.LoadData(tag);

        OldDifficulty = tag.GetByte("oldDifficulty");
    }
}
