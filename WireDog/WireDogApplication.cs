using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WireDog.Common;
using WireDog.Communication;
using WireDog.Enums;
using WireDog.Managers;
using WireDog.Native;
using WireDog.Native.Enums;
using WireDog.Native.Structures;
using WireDog.UI;
using WireDog.UI.Models;

namespace WireDog
{
    public class WireDogApplication
    {
        private readonly MainForm _mainForm;
        private readonly MessageSink _messageSink;
        private readonly HookManager _hookManager;

        public WireDogApplication()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _messageSink = new MessageSink();
            var messageSinkHandle = _messageSink.Handle;
            EnableReceivingWindowMessages(messageSinkHandle);
            _messageSink.OnSocketEvent += HandleSocketEvent;

            _hookManager = new HookManager(messageSinkHandle);

            _mainForm = new MainForm();
            _mainForm.ProcessesRemoved += mainForm_ProcessesRemoved;
            _mainForm.ProcessChecked += mainForm_ProcessChecked;
            Application.Run(_mainForm);
        }

        // UIPI laat standaard geen window messages toe vanaf Windows Vista
        private void EnableReceivingWindowMessages(IntPtr hWnd)
        {
            var changeFilterStruct = new ChangeFilterStruct
            {
                size = (uint)Marshal.SizeOf<ChangeFilterStruct>(),
                info = MessageFilterInfo.None
            };
            NativeMethods.ChangeWindowMessageFilterEx(hWnd, (uint)WindowMessage.CopyData, ChangeWindowMessageFilterExAction.Allow, ref changeFilterStruct);
        }

        private void HandleSocketEvent(SocketEventType socketEventType, IntPtr socketEvent, int socketEventSize)
        {
            switch (socketEventType)
            {
                case SocketEventType.SocketInfo:
                    var socketInfoEvent = Marshal.PtrToStructure<SocketInfoEvent>(socketEvent);
                    _hookManager.AddSocket(socketInfoEvent.ProcessId, socketInfoEvent.SocketDescriptor, socketInfoEvent.LocalAndRemoteSocketAddress);
                    break;

                case SocketEventType.Send:
                case SocketEventType.Recv:
                    HandleSocketPacketEvent(socketEventType, socketEvent, socketEventSize);
                    break;
            }
        }

        private void HandleSocketPacketEvent(SocketEventType socketEventType, IntPtr socketEvent, int socketEventSize)
        {
            var evt = Marshal.PtrToStructure<SocketPacketEvent>(socketEvent);
            HookInfo hookInfo;
            if (!_hookManager.TryGetHookInfo(evt.ProcessId, out hookInfo))
                return;

            var localAndRemoteAddr = hookInfo.GetLocalAndRemoteSocketAddress(evt.SocketDescriptor);

            var packetSize = socketEventSize - 8;
            var packetData = new byte[packetSize];

            Marshal.Copy(socketEvent + 8, packetData, 0, packetSize);

            var socketEventModel = new SocketEventModel
            {
                SocketEventType = socketEventType,
                Timestamp = DateTime.Now,
                ProcessName = hookInfo.ProcessName,
                LocalAddress = localAndRemoteAddr.Local.HostAndPort,
                RemoteAddress = localAndRemoteAddr.Remote.HostAndPort,
                Data = packetData,
                Size = packetSize
            };
            _mainForm.AddSocketEventModelToGrid(socketEventModel);
        }

        private void mainForm_ProcessChecked(Process process, bool isChecked)
        {
            if (isChecked)
                _hookManager.ApplyHooks(process);
            else
                _hookManager.RemoveHooks(process);
        }

        private void mainForm_ProcessesRemoved(IEnumerable<int> processIds)
        {
            foreach (var processId in processIds)
                _hookManager.RemoveProcess(processId);
        }
    }
}
