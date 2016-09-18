using Binarysharp.MemoryManagement;
using WireDog.Enums;
using WireDog.Native;

namespace WireDog.SubRoutines
{
    // void __stdcall SendSocketInfo(SOCKET s);
    public class SendSocketInfo : AbstractSubRoutine
    {
        protected override int AllocationSize
        {
            get { return 150; }
        }

        public SendSocketInfo(MemorySharp memorySharp, BuildCopyData buildCopyData, SendCopyData sendCopyData, GetSocketInfo getSocketInfo)
        {
            var getCurrentProcessId = NativeMethods.GetProcAddressInModule("kernel32.dll", "GetCurrentProcessId");

            Code = new[]{           // stack: ret, s
                "push ebp",         // stack: ebp, ret, s
                "mov ebp, esp",
                "sub esp, 32",
                "mov ebx, [ebp+8]", // SOCKET s
                "push esp",
                "push ebx",
                "call " + getSocketInfo,
                "mov ebx, [ebp+8]", // SOCKET s
                "mov ecx, esp",     // localAndRemoteSockAddr
                "push 32",          // stack: dataLength
                "push ecx",         // stack: localAndRemoteSockAddr, dataLength
                "push ebx",         // stack: s, localAndRemoteSockAddr, dataLength
                "call " + buildCopyData,

                "push eax",         // lpData
                "push 40",          // cbData = 4 + 4 + 16 + 16
                "push " + (int)SocketEventType.SocketInfo, // dwData
                "call " + sendCopyData,
                "add esp, 32",      // stack: ebp, ret, s
                "pop ebp",          // stack: ret, s
                "ret 4"
            };

            Install(memorySharp);
        }
    }
}
