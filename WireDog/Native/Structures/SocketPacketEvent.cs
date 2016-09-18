using System;
using System.Runtime.InteropServices;

namespace WireDog.Native.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public class SocketPacketEvent
    {
        public IntPtr SocketDescriptor;
        public int ProcessId;
    }
}
