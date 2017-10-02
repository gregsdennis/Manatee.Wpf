using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;
// ReSharper disable InconsistentNaming

namespace Manatee.Wpf
{
	internal static class KeyExtensions
	{
		public enum MapType : uint
		{
			MAPVK_VK_TO_VSC,
			MAPVK_VSC_TO_VK,
			MAPVK_VK_TO_CHAR,
			MAPVK_VSC_TO_VK_EX
		}

		private static readonly Dictionary<string, string> WellKnownKeyNameAliases = new Dictionary<string, string>
			{
				{"ctrl", "Control"},
				{"esc", "Escape"}
			};

		[DllImport("user32.dll")]
		public static extern int ToUnicode(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pwszBuff, int cchBuff, uint wFlags);

		[DllImport("user32.dll")]
		public static extern bool GetKeyboardState(byte[] lpKeyState);

		[DllImport("user32.dll")]
		public static extern uint MapVirtualKey(uint uCode, MapType uMapType);

		public static char GetCharFromKey(this Key key)
		{
			var result = ' ';
			var num = KeyInterop.VirtualKeyFromKey(key);
			var lpKeyState = new byte[256];
			GetKeyboardState(lpKeyState);
			var wScanCode = MapVirtualKey((uint) num, MapType.MAPVK_VK_TO_VSC);
			var stringBuilder = new StringBuilder(2);
			switch (ToUnicode((uint) num, wScanCode, lpKeyState, stringBuilder, stringBuilder.Capacity, 0u))
			{
				case -1:
				case 0:
					break;
				case 1:
					result = stringBuilder[0];
					break;
				default:
					result = stringBuilder[0];
					break;
			}
			return result;
		}

		public static Key ToKey(this string keyName)
		{
			var text = _CheckForAlias(keyName);
			int num;
			if (int.TryParse(text, out num))
				text = "D" + text;
			Key result;
			Enum.TryParse(text, out result);
			return result;
		}

		public static ModifierKeys ToModifierKey(this string keyName)
		{
			var text = _CheckForAlias(keyName);
			int num;
			if (int.TryParse(text, out num))
				text = "D" + text;
			ModifierKeys result;
			Enum.TryParse(text, out result);
			return result;
		}

		private static string _CheckForAlias(string alias)
		{
			var key = alias.ToLower();
			if (!WellKnownKeyNameAliases.ContainsKey(key)) return alias;
			return WellKnownKeyNameAliases[key];
		}
	}
}