using ReLogic.Content;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Universium.Common.UI;

public class UIBossFlagsListTab : UIElement
{
    /// <summary>
    ///     The padding of each element in this element in pixels.
    /// </summary>
    public const float ElementPadding = 16f;
    
    /// <summary>
    ///     The width of this element in pixels.
    /// </summary>
    public const float ElementWidth = UIBossFlagsList.ElementWidth;

    /// <summary>
    ///     The height of this element in pixels.
    /// </summary>
    public const float ElementHeight = 48f;
    
    /// <summary>
    ///     The icon of this tab.
    /// </summary>
    public readonly Asset<Texture2D> Icon;

    /// <summary>
    ///     The display name of this tab.
    /// </summary>
    public readonly string Name;

    public UIBossFlagsListTab(Asset<Texture2D> icon, string name) {
        Icon = icon;
        Name = name;
    }

    public override void OnInitialize() {
        base.OnInitialize();
        
        Width.Set(ElementWidth, 0f);
        Height.Set(ElementHeight, 0f);
        
        Append(BuildPanel());
        Append(BuildIcon());
        Append(BuildText());
    }

    private UIPanel BuildPanel() {
        var panel = new UIPanel {
            BackgroundColor = new Color(68, 97, 175) * 0.8f,
            BorderColor = new Color(68, 97, 175) * 0.8f,
            Width = { Pixels = ElementWidth },
            Height = { Pixels = ElementHeight },
            OverrideSamplerState = SamplerState.PointClamp
        };

        return panel;
    }

    private UIImage BuildIcon() {
        var icon = new UIImage(Icon) {
            VAlign = 0.5f,
            Left = { Pixels = ElementPadding },
            OverrideSamplerState = SamplerState.PointClamp
        };

        return icon;
    }

    private UIText BuildText() {
        var text = new UIText(Name, 0.8f) {
            VAlign = 0.5f,
            Left = { Pixels = ElementPadding + Icon.Width() + (32f - Icon.Width()) + ElementPadding }
        };

        return text;
    }
}
