using System.Runtime.InteropServices;

namespace WireDog.Native.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct LocalAndRemoteSocketAddress
    {
        public IPV4SocketAddress Local;
        public IPV4SocketAddress Remote;
    }
}
