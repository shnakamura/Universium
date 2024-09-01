using ReLogic.Content;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Universium.Common.UI;

public sealed class UIBossFlagsList : UIElement
{
    /// <summary>
    ///     The width of this element in pixels.
    /// </summary>
    public const float ElementWidth = UIUniversiumMenu.ListWidth;
    
    /// <summary>
    ///     The height of this element in pixels.
    /// </summary>
    public const float ElementHeight = UIUniversiumMenu.ListHeight;

    public static readonly Asset<Texture2D> UnknownIconTexture = ModContent.Request<Texture2D>($"{nameof(Universium)}/Assets/Textures/UI/Unknown");
    
    /// <summary>
    ///     Whether this element is enabled or not.
    /// </summary>
    public bool Enabled { get; set; }
    
    public override void OnInitialize() {
        base.OnInitialize();
        
        Width.Set(ElementWidth, 0f);
        Height.Set(ElementHeight, 0f);

        Append(BuildPanel());

        var list = BuildList();
        
        Append(list);
        PopulateList(list);
    }
    
    public override void Update(GameTime gameTime) {
        if (!Enabled) {
            return;
        }
        
        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch) {
        if (!Enabled) {
            return;
        }
        
        base.Draw(spriteBatch);
    }
    
    private UIPanel BuildPanel() {
        var panel = new UIPanel(
            ModContent.Request<Texture2D>($"{nameof(Universium)}/Assets/Textures/UI/PanelBackground"),
            ModContent.Request<Texture2D>($"{nameof(Universium)}/Assets/Textures/UI/PanelBorder"),
            13
        ) {
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
        
        var scroll = new UIScrollbar {
            Left = { Pixels = ElementWidth },
            Width = { Pixels = 20f },
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

            var name = npc.TypeName;
            
            var index = npc.GetBossHeadTextureIndex();
            var icon = index == -1 ? UnknownIconTexture : TextureAssets.NpcHeadBoss[index];
            
            list.Add(new UIBossFlagsListTab(icon, name));
        }
    }
}
