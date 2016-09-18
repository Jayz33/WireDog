using Binarysharp.MemoryManagement;
using WireDog.Native;

namespace WireDog.SubRoutines
{
    // void* __stdcall BuildCopyData(SOCKET s, void* data, int dataLength)
    public class BuildCopyData : AbstractSubRoutine
    {
        protected override int AllocationSize
        {
            get { return 150; }
        }

        public BuildCopyData(MemorySharp memorySharp, CopyBuffer copyBuffer)
        {
            var getCurrentProcessId = NativeMethods.GetProcAddressInModule("kernel32.dll", "GetCurrentProcessId");
            var malloc = NativeMethods.GetProcAddressInModule("msvcrt.dll", "malloc");
        
            Code = new[]{               // stack: ret, s, data, dataLength
                "push ebp",             // stack: ebp, ret, s, data, dataLength
                "mov ebp, esp",
                "mov ebx, [ebp+16]",
                "add ebx, 8",
                "push ebx",             // stack: size, ebp, ret, s, data, dataLength
                // void* __cdecl malloc(size_t size);
                "call " + malloc,
                "add esp, 4",           // stack: ebp, ret, s, data, dataLength
                "push eax",             // stack: addr, ebp, ret, s, data, dataLength
                "call " + getCurrentProcessId,
                "mov ebx, [ebp+8]",     // s
                "pop ecx",              // stack: ebp, ret, s, data, dataLength
                "push ecx",             // stack: addr, ebp, ret, s, data, dataLength
                "mov [ecx], ebx",       // s
                "add ecx, 4",
                "mov [ecx], eax",       // processId
                "add ecx, 4",
                "mov ebx, [ebp+12]",    // data (source)
                "mov edx, [ebp+16]",    // dataLength
                "push edx",             // stack: len, addr, ebp, ret, s, data, dataLength
                "push ecx",             // stack: destination, len, addr, ebp, ret, s, data, dataLength
                "push ebx",             // stack: source, destination, len, addr, ebp, ret, s, data, dataLength
                "call " + copyBuffer,   // stack: addr, ebp, ret, s, data, dataLength
                "pop eax",              // stack: ebp, ret, s, data, dataLength
                "pop ebp",              // stack: ret, s, data, dataLength
                "ret 12"
            };

            Install(memorySharp);
        }
    }
}
