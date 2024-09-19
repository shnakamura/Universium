using System.Collections.Generic;
using System.Linq;
using Universium.Common.Config;

namespace Universium.Common.Items;

public sealed class ItemBannings : GlobalItem
{
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
        base.ModifyTooltips(item, tooltips);

        if (!IsBanned(item.type)) {
            return;
        }
        
        tooltips.Add(new TooltipLine(Mod, $"{nameof(Universium)}:{nameof(ItemBannings)}", "This item is banned by the server.") {
            OverrideColor = Color.Gray
        });
    }
    
    public override bool CanResearch(Item item) {
        return !IsBanned(item.type);
    }

    public override bool CanReforge(Item item) {
        return !IsBanned(item.type);
    }

    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded) {
        return !IsBanned(item.type);
    }
    
    public override bool CanRightClick(Item item) {
        return !IsBanned(item.type);
    }
    
    public override bool CanShoot(Item item, Player player) {
        return !IsBanned(item.type);
    }

    public override bool CanUseItem(Item item, Player player) {
        return !IsBanned(item.type);
    }

    private static bool IsBanned(int type) {
        return ServerConfig.Instance.BannedItems.Any(definition => definition.Type == type);
    }
}
