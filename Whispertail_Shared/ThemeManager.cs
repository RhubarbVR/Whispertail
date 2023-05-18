using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Fast.Components.FluentUI.DesignTokens;

namespace Whispertail_Shared
{
	public enum ThemeMode
	{
		Auto,
		Light,
		Dark,
		OledDark
	}

	public sealed class ThemeManager
	{
		internal static Func<Task> _themeChangeRequest;

		public static Task RequestThemeChange() {
			return _themeChangeRequest?.Invoke();
		}

		public ThemeMode Mode { get; set; } = ThemeMode.Auto;

		public float StrokeWidth { get; set; } = 1f;

		public int CornerRadius { get; set; } = 15;

		public Swatch AccentColor { get; set; } = "#ffb858".ToSwatch();
		public Swatch NeutralColor { get; set; } = "#ffffff".ToSwatch();

		private readonly BaseLayerLuminance _baseLayerLuminance;
		private readonly AccentBaseColor _accentBaseColor;
		private readonly NeutralBaseColor _neutralBaseColor;
		private readonly StrokeWidth _strokeWidth;
		private readonly ControlCornerRadius _controlCornerRadius;
		private readonly ILocalStorageService _localStorageService;


		public async Task SaveSettings() {
			await _localStorageService.SetItemAsStringAsync("theme_Mode", Mode.ToString());
			await _localStorageService.SetItemAsStringAsync("theme_StrokeWidth", StrokeWidth.ToString());
			await _localStorageService.SetItemAsStringAsync("theme_CornerRadius", CornerRadius.ToString());
			await _localStorageService.SetItemAsStringAsync("theme_AccentColor", SwatchToHtml(AccentColor));
			await _localStorageService.SetItemAsStringAsync("theme_NeutralColor", SwatchToHtml(NeutralColor));
		}

		private static string SwatchToHtml(Swatch swatch) {
			return ColorTranslator.ToHtml(System.Drawing.Color.FromArgb((byte)(255f * swatch.R), (byte)(255f * swatch.G), (byte)(255f * swatch.B)));
		}

		private static int? ParseInt(string data) {
			return int.TryParse(data, out var returndata) ? returndata : null;
		}

		private static float? ParseFloat(string data) {
			return float.TryParse(data, out var returndata) ? returndata : null;
		}

		private static ThemeMode? ParseMode(string data) {
			return Enum.TryParse<ThemeMode>(data, out var returndata) ? returndata : null;
		}

		private async Task LoadSettings() {
			try {
				Mode = ParseMode(await _localStorageService.GetItemAsStringAsync("theme_Mode")) ?? Mode;
			}
			catch { }
			try {
				StrokeWidth = ParseFloat(await _localStorageService.GetItemAsStringAsync("theme_StrokeWidth")) ?? StrokeWidth;
			}
			catch { }
			try {
				CornerRadius = ParseInt(await _localStorageService.GetItemAsStringAsync("theme_CornerRadius")) ?? CornerRadius;
			}
			catch { }
			try {
				AccentColor = (await _localStorageService.GetItemAsStringAsync("theme_AccentColor") ?? SwatchToHtml(AccentColor)).ToSwatch();
			}
			catch { }
			try {
				NeutralColor = (await _localStorageService.GetItemAsStringAsync("theme_NeutralColor") ?? SwatchToHtml(NeutralColor)).ToSwatch();
			}
			catch { }
			await SaveSettings();
		}

		public async Task UpdateTheme(ElementReference elementReference, bool IsDarkMode) {
			await LoadSettings();
			var localMode = Mode;

			if (localMode == ThemeMode.Auto) {
				localMode = IsDarkMode ? ThemeMode.Dark : ThemeMode.Light;
			}
			await _neutralBaseColor.SetValueFor(elementReference, NeutralColor);

			switch (localMode) {
				case ThemeMode.Light:
					await _baseLayerLuminance.SetValueFor(elementReference, (float)0.85);
					break;
				case ThemeMode.OledDark:
					await _baseLayerLuminance.SetValueFor(elementReference, 0);
					break;
				default:
					await _baseLayerLuminance.SetValueFor(elementReference, (float)0.15);
					break;
			}

			await _accentBaseColor.SetValueFor(elementReference, AccentColor);
			await _strokeWidth.SetValueFor(elementReference, StrokeWidth);
			await _controlCornerRadius.SetValueFor(elementReference, CornerRadius);
		}

		public ThemeManager(BaseLayerLuminance baseLayerLuminance, AccentBaseColor AccentBaseColor, NeutralBaseColor neutralBaseColor, StrokeWidth StrokeWidth, ControlCornerRadius ControlCornerRadius, ILocalStorageService localStorageService) {
			_baseLayerLuminance = baseLayerLuminance;
			_accentBaseColor = AccentBaseColor;
			_neutralBaseColor = neutralBaseColor;
			_strokeWidth = StrokeWidth;
			_controlCornerRadius = ControlCornerRadius;
			_localStorageService = localStorageService;
		}
	}
}
