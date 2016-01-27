using System;
using System.Runtime.InteropServices;

namespace w20620.Win32
{
    internal static class Win32
    {
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);
    }
}