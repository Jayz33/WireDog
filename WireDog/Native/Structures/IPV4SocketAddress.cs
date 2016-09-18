using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Linq;

namespace WireDog.Native.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IPV4SocketAddress
    {
        public ushort AddressFamily;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] PortInBytes;
        public uint Address;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] sin_zero;

        public string Host
        {
            get
            {
                return new IPAddress(BitConverter.GetBytes(Address)).ToString();
            }
        }

        public ushort Port
        {
            get
            {
                return BitConverter.ToUInt16(PortInBytes.Reverse().ToArray(), 0);
            }
        }

        public string HostAndPort
        {
            get
            {
                return Host + ":" + Port;
            }
        }
    }
}
