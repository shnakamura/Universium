using Microsoft.Xna.Framework.Input;
using Terraria.DataStructures;
using Universium.Utilities;

namespace Universium.Common.Effects;

public sealed class PlayerDeathChest : ModPlayer
{
    public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
        base.Kill(damage, hitDirection, pvp, damageSource);

        var x = (int)(Player.position.X / 16f);
        var y = (int)(Player.position.Y / 16f);

        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 3; j++) {
                WorldGen.KillTile(x + i, y + j);
            }
                
            WorldGen.PlaceTile(x + i, y + 3, TileID.Obsidian, true, true);
        }

        var index = WorldGen.PlaceChest(x, y + 2);
            
        if (index == -1) {
            return;
        }
            
        var chest = Main.chest[index];

        for (int i = 0; i < Player.inventory.Length; i++) {
            ref var item = ref Player.inventory[i];
            var clone = item.Clone();

            if (!chest.TryAddItem(clone)) {
                Player.DropItem(Player.GetSource_Death(), new Vector2(x, y) * 16f, ref item);
            }
            else {
                item.TurnToAir();
            }
        }
    }
}
