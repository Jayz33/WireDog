using System;
using System.Runtime.InteropServices;

namespace WireDog.Native.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CopyDataStruct
    {
        public IntPtr dwData;
        public int cbData;
        public IntPtr lpData;
    }
}
