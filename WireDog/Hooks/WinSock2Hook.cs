using Binarysharp.MemoryManagement;
using System;
using WireDog.Native;
using WireDog.SubRoutines;

namespace WireDog.Hooks
{
    public abstract class WinSock2Hook : AbstractHook
    {
        protected IntPtr WinSock2ModuleHandle
        {
            get { return Native.NativeMethods.GetModuleHandle("ws2_32.dll"); }
        }

        protected IsSocketKnown IsSocketKnown { get; set; }
        protected SetSocketKnown SetSocketKnown { get; set; }
        protected SendSocketInfo SendSocketInfo { get; set; }
        protected SendCopyData SendCopyData { get; set; }
        protected CopyBuffer CopyBuffer { get; set; }
        protected BuildCopyData BuildCopyData { get; set; }
        protected SendPacketData SendPacketData { get; set; }

        private readonly IntPtr _socketDescriptorsAddress;
        private readonly IntPtr _sendMessageDestinationHandle;

        public WinSock2Hook(MemorySharp memorySharp, IntPtr sendMessageDestinationHandle, IntPtr socketDescriptorsAddress)
        {
            _sendMessageDestinationHandle = sendMessageDestinationHandle;
            _socketDescriptorsAddress = socketDescriptorsAddress;
            InstallSubRoutines(memorySharp);
        }

        private void InstallSubRoutines(MemorySharp memorySharp)
        {
            IsSocketKnown = new IsSocketKnown(memorySharp, _socketDescriptorsAddress);

            SetSocketKnown = new SetSocketKnown(memorySharp, _socketDescriptorsAddress);

            var getSocketInfo = new GetSocketInfo(memorySharp);

            SendCopyData = new SendCopyData(memorySharp, _sendMessageDestinationHandle);

            CopyBuffer = new CopyBuffer(memorySharp);

            BuildCopyData = new BuildCopyData(memorySharp, CopyBuffer);

            SendSocketInfo = new SendSocketInfo(memorySharp, BuildCopyData, SendCopyData, getSocketInfo);

            SendPacketData = new SendPacketData(memorySharp, BuildCopyData, SendCopyData);
        }
    }
}
