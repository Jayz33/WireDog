using Binarysharp.MemoryManagement;
using System;

namespace WireDog.SubRoutines
{
    // bool __stdcall IsSocketKnown(SOCKET s);
    public class IsSocketKnown : AbstractSubRoutine
    {
        protected override int AllocationSize
        {
            get { return 100; }
        }

        public IsSocketKnown(MemorySharp memorySharp, IntPtr socketDescriptorsAddress)
        {
            Code = new[]{           // stack: ret, s
                "push ebp",         // stack: ebp, ret, s
                "mov ebp, esp",
                "mov ebx, [ebp+8]", // SOCKET s
                "mov eax, 1",
                "mov ecx, " + socketDescriptorsAddress,
                "compareLoop:",
                "mov edx, [ecx]",
                "test edx, edx",
                "jz returnFalse",
                "cmp edx, ebx",
                "mov ecx, [ecx+4]",
                "jnz compareLoop",
                "return:",
                "pop ebp",
                "ret 4",
                "returnFalse:",
                "mov eax, 0",
                "jmp return"
            };

            Install(memorySharp);
        }
    }
}
