using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WireDog.Hooks;
using WireDog.Native.Structures;

namespace WireDog.Common
{
    public struct HookInfo
    {
        public MemorySharp MemorySharp { get; set; }
        public Process Process { get; set; }
        public string ProcessName { get; set; }
        public IEnumerable<AbstractHook> Hooks { get; set; }
        public IDictionary<IntPtr, LocalAndRemoteSocketAddress> Sockets { get; set; }

        public LocalAndRemoteSocketAddress GetLocalAndRemoteSocketAddress(IntPtr socketDescriptor)
        {
            LocalAndRemoteSocketAddress localAndRemoteSocketAddress;
            if (!Sockets.TryGetValue(socketDescriptor, out localAndRemoteSocketAddress))
                return default(LocalAndRemoteSocketAddress);
            return localAndRemoteSocketAddress;
        }

    }
}
