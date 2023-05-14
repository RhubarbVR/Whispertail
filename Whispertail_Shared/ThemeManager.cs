using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public static Func<Task> ThemeChangeRequest;

		public static Task RequestThemeChange() {
			return ThemeChangeRequest?.Invoke();
		}

		public ThemeMode Mode { get; set; } = ThemeMode.Auto;

		public float StrokeWidth { get; set; } = 1f;

		public int CornerRadius { get; set; } = 15;

		public Swatch AccentColor { get; set; } = "#ff8800".ToSwatch();
		public Swatch NeutralColor { get; set; } = "#ffffff".ToSwatch();

		private readonly BaseLayerLuminance _baseLayerLuminance;
		private readonly AccentBaseColor _accentBaseColor;
		private readonly NeutralBaseColor _neutralBaseColor;
		private readonly StrokeWidth _strokeWidth;
		private readonly ControlCornerRadius _controlCornerRadius;

		public async Task UpdateTheme(ElementReference elementReference,bool IsDarkMode) {
			var localMode = Mode;

			if(localMode == ThemeMode.Auto) {
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

		public ThemeManager(BaseLayerLuminance baseLayerLuminance, AccentBaseColor AccentBaseColor, NeutralBaseColor neutralBaseColor, StrokeWidth StrokeWidth, ControlCornerRadius ControlCornerRadius) {
			_baseLayerLuminance = baseLayerLuminance;
			_accentBaseColor = AccentBaseColor;
			_neutralBaseColor = neutralBaseColor;
			_strokeWidth = StrokeWidth;
			_controlCornerRadius = ControlCornerRadius;
		}
	}
}
