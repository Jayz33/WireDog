using System;
using System.Runtime.InteropServices;
using WireDog.Native.Enums;
using WireDog.Native.Structures;

namespace WireDog.Native
{
    public static class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ChangeWindowMessageFilterEx(IntPtr hWnd, uint msg,
        ChangeWindowMessageFilterExAction action, ref ChangeFilterStruct changeInfo);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process(
             [In] IntPtr hProcess,
             [Out, MarshalAs(UnmanagedType.Bool)] out bool wow64Process
             );

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string method);

        public static IntPtr GetProcAddressInModule(string lpModuleName, string method)
        {
            return GetProcAddress(GetModuleHandle(lpModuleName), method);
        }
    }
}
