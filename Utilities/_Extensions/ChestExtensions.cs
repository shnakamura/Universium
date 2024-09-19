using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Universium.Utilities;

public static class ChestExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasItem(this Chest chest, int type) {
        for (var i = 0; i < Chest.maxItems; i++) {
            var item = chest.item[i];

            if (!item.IsAir && item.type == type) {
                return true;
            }
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryAddItem(this Chest chest, int type, int stack, bool randomSlot = false) {
        var item = new Item();
        
        item.SetDefaults(type);
        item.stack = stack;
        
        return chest.TryAddItem(item, randomSlot);
    }
    
    public static bool TryAddItem(this Chest chest, Item item, bool randomSlot = false) {
        if (!chest.TryGetEmptySlot(out var index, randomSlot) || item.type == ItemID.None) {
            return false;
        }

        chest.item[index] = item;

        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGetEmptySlot(this Chest chest, out int index, bool randomSlot) {
        var indices = new List<int>();

        for (var i = 0; i < Chest.maxItems; i++) {
            var item = chest.item[i];

            if (item != null && item.IsAir) {
                indices.Add(i);
            }
        }

        if (indices.Count <= 0) {
            index = -1;

            return false;
        }

        index = randomSlot ? Main.rand.Next(indices) : indices[0];

        return true;
    }
}
