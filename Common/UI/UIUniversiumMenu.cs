using Microsoft.Xna.Framework.Input;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Universium.Common.Utilities;

namespace Universium.Common.UI;

public sealed class UIUniversiumMenu : UIState
{
    /// <summary>
    ///     The unique identifier of this state.
    /// </summary>
    public const string Identifier = $"{nameof(Universium)}:{nameof(UIUniversiumMenu)}";
    
    /// <summary>
    ///     The width of each button of this state in pixels.
    /// </summary>
    public const float ButtonWidth = 32f;

    /// <summary>
    ///     The height of each button of this state in pixels.
    /// </summary>
    public const float ButtonHeight = 32f;

    /// <summary>
    ///     The padding of each element in this state in pixels.
    /// </summary>
    public const float ElementPadding = 8f;

    /// <summary>
    ///     The margin of each element in this state in pixels.
    /// </summary>
    public const float ElementMargin = 16f;
    
    /// <summary>
    ///     The height of each list element in this state in pixels.
    /// </summary>
    public const float ListHeight = 200f;

    /// <summary>
    ///     The width of this state in pixels.
    /// </summary>
    public const float StateWidth = (ButtonWidth + ElementPadding) * 4f;

    /// <summary>
    ///     The height of this state in pixels.
    /// </summary>
    public const float StateHeight = ButtonHeight + ListHeight + ElementPadding;
    
    /// <summary>
    ///     Whether the state is enabled or not.
    /// </summary>
    public bool Enabled { get; set; }

    private UIElement root;
    
    public override void OnInitialize() {
        base.OnInitialize();

        root = new UIElement {
            Width = { Pixels = StateWidth },
            Height = { Pixels = StateHeight },
            HAlign = 0.5f,
            VAlign = 0.5f
        };
        
        root.OnUpdate += RootUpdateEvent;
        
        Append(root);

        root.Append(BuildButtonPanel());
        root.Append(BuildFlagsButton());
    }

    private void RootUpdateEvent(UIElement element) {
        if (Main.keyState.IsKeyDown(Keys.F) && !Main.oldKeyState.IsKeyDown(Keys.F)) {
            Enabled = !Enabled;
        }
        
        ref var pixels = ref element.Top.Pixels;
        var target = Enabled ? 0f : Main.screenHeight / 2f + StateHeight;

        pixels = MathHelper.SmoothStep(pixels, target, 0.3f);
    }

    private UIPanel BuildButtonPanel() {
        var panel = new UIPanel {
            BackgroundColor = new Color(41, 66, 133) * 0.8f,
            BorderColor = new Color(13, 13, 15),
            VAlign = 1f,
            Width = { Pixels = StateWidth },
            Height = { Pixels = ButtonHeight },
            OverrideSamplerState = SamplerState.PointClamp
        };

        return panel;
    }

    private UIHoverTooltipImage BuildFlagsButton() {
        var button = new UIHoverTooltipImage(TextureAssets.Item[ItemID.HermesBoots], $"Mods.{nameof(Universium)}.UI.Buttons.Flags") {
            ActiveScale = 1.25f,
            VAlign = 1f,
            OverrideSamplerState = SamplerState.PointClamp,
        };

        button.OnLeftClick += ButtonLeftClickEvent; 
        
        return button;
    }

    private void ButtonLeftClickEvent(UIMouseEvent @event, UIElement element) {
        root.Toggle(new UIBossFlagsList {
            HAlign = 0.5f
        });
    }
}
