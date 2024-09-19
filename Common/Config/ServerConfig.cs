using System.Collections.Generic;
using Terraria.ModLoader.Config;

namespace Universium.Common.Config;

public sealed class ServerConfig : ModConfig
{
    public static ServerConfig Instance => ModContent.GetInstance<ServerConfig>();
    
    public override ConfigScope Mode { get; } = ConfigScope.ServerSide;

    [Header("Settings")]
    public List<NPCDefinition> BannedNPCs = new();
    public List<ItemDefinition> BannedItems = new();
}
