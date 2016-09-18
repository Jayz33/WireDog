using Binarysharp.MemoryManagement;
using WireDog.Native;

namespace WireDog.SubRoutines
{
    // void __stdcall SendPacketData(SOCKET s, PacketEventMessage msg, byte* buf, int len)
    // todo: herbruikbare code voor alle PacketEventMessages en het opstellen van processId & socketDescriptor bovenaan de copyData
    public class SendPacketData : AbstractSubRoutine
    {
        protected override int AllocationSize
        {
            get { return 150; }
        }

        public SendPacketData(MemorySharp memorySharp, BuildCopyData buildCopyData, SendCopyData sendCopyData)
        {
            var free = NativeMethods.GetProcAddressInModule("msvcrt.dll", "free");

            Code = new[]{               // stack: ret, s, msg, buf, len
                "push ebp",             // stack: ebp, ret, s, msg, buf, len
                "mov ebp, esp",
                "mov ebx, [ebp+8]",     // s
                "mov ecx, [ebp+16]",    // buf
                "mov edx, [ebp+20]",    // len
                "push edx",             // stack: len, ret, s, msg, buf, len
                "push ecx",             // stack: buf, len, ret, s, msg, buf, len
                "push ebx",             // stack: s, buf, len, ret, s, msg, buf, len
                "call " + buildCopyData,// stack: ret, s, msg, buf, len
                "mov ebx, [ebp+12]",     // msg
                "mov ecx, [ebp+20]",    // len
                "add ecx, 8",           // sizeof(SOCKET) + sizeof(int)
                "push eax",             // lpData om achteraf te kunnen vrijgeven
                "push eax",             // lpData
                "push ecx",             // cbData
                "push ebx",             // dwData
                "call " + sendCopyData, // stack: lpData, ebp, ret, s, msg, buf, len
                // void __cdecl free(void* memblock)
                "call " + free,         // stack: lpData, ebp, ret, s, msg, buf, len
                "add esp, 4",           // stack: ebp, ret, s, msg, buf, len
                "pop ebp",              // stack: ret, s, msg, buf, len
                "ret 16"
            };

            Install(memorySharp);
        }
    }
}
