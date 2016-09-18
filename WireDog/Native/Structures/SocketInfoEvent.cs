using System;
using System.Runtime.InteropServices;

namespace WireDog.Native.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SocketInfoEvent
    {
        public IntPtr SocketDescriptor;
        public int ProcessId;
        public LocalAndRemoteSocketAddress LocalAndRemoteSocketAddress;
    }
}
