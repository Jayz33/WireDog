using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WireDog.Common;
using WireDog.Hooks;
using WireDog.Native.Structures;

namespace WireDog.Managers
{
    public class HookManager : IDisposable
    {
        private readonly IDictionary<int, HookInfo> _hookedProcesses;
        private readonly IntPtr _sendMessageDestinationHandle;

        public HookManager(IntPtr sendMessageDestinationHandle)
        {
            _hookedProcesses = new Dictionary<int, HookInfo>();
            _sendMessageDestinationHandle = sendMessageDestinationHandle;
        }

        public void ApplyHooks(Process process)
        {
            if (_hookedProcesses.ContainsKey(process.Id))
                return;

            var memorySharp = new MemorySharp(process);
        
            var socketDescriptorsLinkedListHead = memorySharp.Memory.Allocate(8);

            var hooks = new List<AbstractHook> 
            { 
                new WinSock2SendHook(memorySharp, _sendMessageDestinationHandle, socketDescriptorsLinkedListHead.BaseAddress),
                new WinSock2RecvHook(memorySharp, _sendMessageDestinationHandle, socketDescriptorsLinkedListHead.BaseAddress) 
            };

            var hookInfo = new HookInfo
            {
                MemorySharp = memorySharp,
                Process = process,
                ProcessName = string.Format("{0} ({1})", Path.GetFileName(process.MainModule.FileName), process.Id),
                Hooks = hooks,
                Sockets = new Dictionary<IntPtr, LocalAndRemoteSocketAddress>()
            };

            _hookedProcesses.Add(process.Id, hookInfo);
        }

        public void RemoveHooks(Process process)
        {
            HookInfo hookInfo;
            if (!_hookedProcesses.TryGetValue(process.Id, out hookInfo))
                return;

            var memorySharp = hookInfo.MemorySharp;

            foreach (var hook in hookInfo.Hooks)
            {
                hook.Remove(memorySharp);
            }

            memorySharp.Dispose(); // zou ook alle RemoteAllocations terug moeten vrijgeven

            RemoveProcess(process.Id);
        }

        public void AddSocket(int processId, IntPtr socketDescriptor, LocalAndRemoteSocketAddress localAndRemoteSocketAddress)
        {
            HookInfo hookInfo;
            if (!_hookedProcesses.TryGetValue(processId, out hookInfo))
                return;
            hookInfo.Sockets.Add(socketDescriptor, localAndRemoteSocketAddress);
        }

        public bool TryGetHookInfo(int processId, out HookInfo hookInfo)
        {
            return _hookedProcesses.TryGetValue(processId, out hookInfo);
        }

        public LocalAndRemoteSocketAddress GetLocalAndRemoteSockAddr(int processId, IntPtr socketDescriptor)
        {
            HookInfo hookInfo;
            LocalAndRemoteSocketAddress localAndRemoteSockAddr;
            if (!_hookedProcesses.TryGetValue(processId, out hookInfo)
                || !hookInfo.Sockets.TryGetValue(socketDescriptor, out localAndRemoteSockAddr))
                return default(LocalAndRemoteSocketAddress);

            return localAndRemoteSockAddr;
        }

        public void RemoveProcess(int id)
        {
            _hookedProcesses.Remove(id);
        }

        public void Dispose()
        {
            foreach (var hookInfo in _hookedProcesses.Values.ToList())
            {
                RemoveHooks(hookInfo.Process);
            }
        }
    }
}
