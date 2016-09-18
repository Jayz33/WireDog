using Binarysharp.MemoryManagement;
using WireDog.Enums;

namespace WireDog.SubRoutines
{
    // void __stdcall RecvCallback(ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags)
    public class RecvCallback : AbstractSubRoutine
    {
        protected override int AllocationSize
        {
            get { return 150; }
        }

        public RecvCallback(MemorySharp memorySharp, IsSocketKnown isSocketKnown, SetSocketKnown setSocketKnown,
            SendSocketInfo sendSocketInfo, SendPacketData sendPacketData)
        {
            Code = new[]{                       // stack: ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "push eax",                     // stack: eax, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "cmp eax, 0",                   // indien 0 bytes gelezen hoeven we niets te doen
                "je cleanUpAndReturn",
                "lea ebp, [esp+28]",            
                "mov edx, [ebp]",
                "push edx",                     // stack: s, eax, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "call " + isSocketKnown,        // stack: eax, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "test eax, 1",
                "jnz continue",
                "mov edx, [ebp]",
                "push edx",                     // stack: s, eax, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "call " + setSocketKnown,       // stack: eax, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "mov edx, [ebp]",               
                "push edx",                     // stack: s, eax, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "call " + sendSocketInfo,       // stack: eax, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "continue:",
                "mov ebx, [ebp]",               // s
                "mov ecx, [ebp+4]",             // buf
                "mov edx, [ebp+8]",             // len
                "push edx",                     // stack: len, eax, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "push ecx",                     // stack: buf, len, eax, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "push " + (int) SocketEventType.Recv, // stack: msg, buf, len, eax, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "push ebx",                     // stack: s, msg, buf, len, eax, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "call " + sendPacketData,       // stack: eax, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "cleanUpAndReturn:",
                "pop eax",                      // belangrijk: returnwaarde herstellen (aantal gelezen bytes) want de caller verwacht dit
                "pop ebx",
                "pop ecx",
                "pop edx",
                "pop ebp",                      // stack: ret_ws32, ret_mainmodule, s, buf, len, flags
                "add esp, 4",                   // stack: ret_mainmodule, s, buf, len, flags
                "ret 16"
            };

            Install(memorySharp);
        }
    }
}
