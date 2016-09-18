using Binarysharp.MemoryManagement;
using System;
using WireDog.Native;
using WireDog.Native.Enums;

namespace WireDog.SubRoutines
{
    // void __stdcall SendCopyData(dwData, cbData, lpData)
    public class SendCopyData : AbstractSubRoutine
    {
        private IntPtr _sendMessage = NativeMethods.GetProcAddressInModule("user32.dll", "SendMessageW");

        protected override int AllocationSize
        {
            get { return 100; }
        }

        public SendCopyData(MemorySharp memorySharp, IntPtr sendMessageDestinationHandle)
        {
            Code = new[]{                               // stack: ret, dwData, cbData, lpData
                "lea ebx, [esp+4]",
                "push ebx",                             // lParam
                "push 0",                               // wParam
                "push " + (int) WindowMessage.CopyData, // WM_COPYDATA
                "push " + sendMessageDestinationHandle, // hWnd
                "call " + _sendMessage,                 // stack: ret, dwData, cbData, lpData
                "pop ebx",                              // stack: dwData, cbData, lpData
                "add esp, 12",                          // stack:
                "push ebx",                             // stack: ret
                "ret"
            };

            Install(memorySharp);
        }
    }
}
