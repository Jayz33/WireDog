using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WireDog.Delegation;
using WireDog.Enums;
using WireDog.Native.Enums;
using WireDog.Native.Structures;

namespace WireDog.Communication
{
    public class MessageSink : Control
    {
        public event SocketEventHandler OnSocketEvent;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int) WindowMessage.CopyData)
            {
                var copyData = Marshal.PtrToStructure<CopyDataStruct>(m.LParam);
                OnSocketEvent((SocketEventType)copyData.dwData, copyData.lpData, copyData.cbData);
            }
            base.WndProc(ref m);
        }
    }
}
