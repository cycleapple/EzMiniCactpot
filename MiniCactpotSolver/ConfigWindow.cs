using System.Numerics;
using Dalamud.Bindings.ImGui;
using KamiLib.Classes;
using KamiLib.Window;

namespace MiniCactpotSolver;

public class ConfigWindow() : Window("EzMiniCactpot", new Vector2(400.0f, 500.0f)) {

	protected override void DrawContents() {
		DrawAnimationConfig();
		DrawIconConfig();
		DrawColorConfig();
	}

	private static void DrawAnimationConfig() {
		ImGuiTweaks.Header("動畫");

		if (ImGui.Checkbox("啟用動畫", ref Service.Config.EnableAnimations)) {
			if (Service.Config.EnableAnimations) {
				Service.AddonController.EnableAnimations();
			}
			else {
				Service.AddonController.DisableAnimations();
			}
		}
	}
	
	private void DrawIconConfig() {
		ImGuiTweaks.Header("圖示");

		if (ImGuiTweaks.GameIconButton(Service.TextureProvider, 61332)) {
			Service.Config.IconId = 61332;
			UpdateIcons();
		}

		ImGui.SameLine();
		
		if (ImGuiTweaks.GameIconButton(Service.TextureProvider, 90452)) {
			Service.Config.IconId = 90452;
			UpdateIcons();
		}
		
		ImGui.SameLine();
		
		if (ImGuiTweaks.GameIconButton(Service.TextureProvider, 234008)) {
			Service.Config.IconId = 234008;
			UpdateIcons();
		}
		
		ImGui.Spacing();
		
		ImGui.AlignTextToFramePadding();
		ImGui.Text("圖示 ID：");
		
		ImGui.SameLine();
		
		var iconId = (int) Service.Config.IconId;
		if (ImGui.InputInt("##IconId", ref iconId)) {
			Service.Config.IconId = (uint) iconId;
			UpdateIcons();
		}
	}
	
	private void DrawColorConfig() {
		ImGuiTweaks.Header("顏色");

		if (ImGui.ColorEdit4("按鈕顏色", ref Service.Config.ButtonColor, ImGuiColorEditFlags.AlphaPreviewHalf)) {
			UpdateColors();
		}

		if (ImGui.ColorEdit4("路線顏色", ref Service.Config.LaneColor, ImGuiColorEditFlags.AlphaPreviewHalf)) {
			UpdateColors();
		}
	}

	public override void OnClose()
		=> Service.Config.Save();

	private void UpdateIcons()
		=> Service.AddonController.UpdateIcons(Service.Config.IconId);

	private void UpdateColors() {
		Service.AddonController.UpdateButtonColors(Service.Config.ButtonColor);
		Service.AddonController.UpdateLaneColors(Service.Config.LaneColor);
	}
}
