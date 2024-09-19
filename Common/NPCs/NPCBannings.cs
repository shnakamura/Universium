using System.Collections.Generic;
using System.Linq;
using Universium.Common.Config;

namespace Universium.Common.NPCs;

public sealed class NPCBannings : GlobalNPC
{
    public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo) {
        base.EditSpawnPool(pool, spawnInfo);

        foreach (var type in pool.Keys) {
            if (IsBanned(type)) {
                pool[type] = 0f;
            }
        }
    }
    
    public override bool PreAI(NPC npc) {
        if (IsBanned(npc.type)) {
            npc.active = false;
            return false;
        }

        return true;
    }
    
    private static bool IsBanned(int type) {
        return ServerConfig.Instance.BannedNPCs.Any(definition => definition.Type == type);
    }
}
