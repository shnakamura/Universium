using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Universium.Common.UI;

public sealed class UIBossFlagsList : UIElement
{
    /// <summary>
    ///     The width of this element in pixels.
    /// </summary>
    public const float ElementWidth = UIUniversiumMenu.StateWidth;
    
    /// <summary>
    ///     The height of this element in pixels.
    /// </summary>
    public const float ElementHeight = UIUniversiumMenu.ListHeight;

    private UIList list;
    
    public override void OnInitialize() {
        base.OnInitialize();
        
        Width.Set(ElementWidth, 0f);
        Height.Set(ElementHeight, 0f);
        
        Append(BuildPanel());

        list = BuildList();
        
        Append(list);
        PopulateList(list);
    }
    
    private UIPanel BuildPanel() {
        var panel = new UIPanel {
            BackgroundColor = new Color(41, 66, 133) * 0.8f,
            BorderColor = new Color(13, 13, 15),
            Width = { Pixels = ElementWidth },
            Height = { Pixels = ElementHeight },
            OverrideSamplerState = SamplerState.PointClamp,
        };

        return panel;
    }
    
    private UIList BuildList() {
        var list = new UIList {
            ListPadding = 0f,
            Width = { Pixels = ElementWidth },
            Height = { Pixels = ElementHeight },
            OverrideSamplerState = SamplerState.PointClamp
        };

        Append(list);

        var scroll = new UIScrollbar {
            Left = { Pixels = ElementWidth },
            Height = { Pixels = ElementHeight },
            OverrideSamplerState = SamplerState.PointClamp
        };

        list.Append(scroll);
        list.SetScrollbar(scroll);

        return list;
    }

    private void PopulateList(UIList list) {
        for (int i = 0; i < NPCLoader.NPCCount; i++) {
            var npc = ContentSamples.NpcsByNetId[i];

            if (!npc.boss) {
                continue;
            }

            var icon = TextureAssets.NpcHeadBoss[npc.GetBossHeadTextureIndex()];
            var name = npc.TypeName;

            var tab = new UIBossFlagsListTab(icon, name);
            
            list.Add(tab);
        }
    }
}
