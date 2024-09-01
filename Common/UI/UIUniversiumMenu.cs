using Microsoft.Xna.Framework.Input;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;

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
    public const float ButtonWidth = 32f + ElementMargin;

    /// <summary>
    ///     The height of each button of this state in pixels.
    /// </summary>
    public const float ButtonHeight = 32f + ElementMargin;

    /// <summary>
    ///     The padding of each element in this state in pixels.
    /// </summary>
    public const float ElementPadding = 8f;

    /// <summary>
    ///     The margin of each element in this state in pixels.
    /// </summary>
    public const float ElementMargin = 16f;
    
    /// <summary>
    ///     The width of each list element in this state in pixels.
    /// </summary>
    public const float ListWidth = 300f;
    
    /// <summary>
    ///     The height of each list element in this state in pixels.
    /// </summary>
    public const float ListHeight = 400f;
    
    /// <summary>
    ///     The width of this state in pixels.
    /// </summary>
    public const float StateWidth = (ButtonWidth + ElementPadding) * 4f + ListWidth;

    /// <summary>
    ///     The height of this state in pixels.
    /// </summary>
    public const float StateHeight = ButtonHeight + ListHeight + ElementPadding;
    
    /// <summary>
    ///     Whether the state is enabled or not.
    /// </summary>
    public bool Enabled { get; set; }

    private UIElement root;
    private UIPanel panel;
    private UIBossFlagsList list;
    
    public override void OnInitialize() {
        base.OnInitialize();

        root = BuildRoot();
        
        Append(root);

        list = BuildFlagsList();
        root.Append(list);

        panel = BuildPanel();
        root.Append(panel);
        
        panel.Append(BuildFlagsButton());
    }

    private void RootUpdateEvent(UIElement element) {
#if DEBUG
        if (Main.keyState.IsKeyDown(Keys.F) && !Main.oldKeyState.IsKeyDown(Keys.F)) {
            Enabled = !Enabled;
        }
#endif
        
        ref var pixels = ref element.Top.Pixels;

        pixels = MathHelper.SmoothStep(pixels, Enabled ? 0f : Main.screenHeight / 2f + StateHeight, 0.3f);
    }

    private UIElement BuildRoot() {
        var root = new UIElement() {
            Width = { Pixels = StateWidth },
            Height = { Pixels = StateHeight },
            HAlign = 0.5f,
            VAlign = 0.5f,
            Top = { Pixels = Main.screenHeight / 2f + StateHeight }
        };
        
        root.OnUpdate += RootUpdateEvent;

        return root;
    }

    private UIBossFlagsList BuildFlagsList() {
        list = new UIBossFlagsList {
            HAlign = 0.5f,
            VAlign = 0f,
        };

        return list;
    }

    private UIPanel BuildPanel() {
        var panel = new UIPanel(
            ModContent.Request<Texture2D>($"{nameof(Universium)}/Assets/Textures/UI/PanelBackground"),
            ModContent.Request<Texture2D>($"{nameof(Universium)}/Assets/Textures/UI/PanelBorder"),
            13
        ) {
            BackgroundColor = new Color(41, 66, 133) * 0.8f,
            BorderColor = new Color(13, 13, 15),
            HAlign = 0.5f,
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
            VAlign = 0.5f,
            OverrideSamplerState = SamplerState.PointClamp,
        };

        button.OnLeftClick += FlagsButtonLeftClickEvent; 
        
        button.OnMouseOver += ButtonMouseOverEvent;
        button.OnMouseOut += ButtonMouseOutEvent;
        
        return button;
    }

    private void FlagsButtonLeftClickEvent(UIMouseEvent @event, UIElement element) {
        list.Enabled = !list.Enabled;
    }
    
    private static void ButtonMouseOverEvent(UIMouseEvent @event, UIElement element) {
        SoundEngine.PlaySound(SoundID.MenuTick with {
            Pitch = 0.25f
        });
    }
    
    private static void ButtonMouseOutEvent(UIMouseEvent @event, UIElement element) {
        SoundEngine.PlaySound(SoundID.MenuTick with {
            Pitch = -0.15f
        });
    }
}
