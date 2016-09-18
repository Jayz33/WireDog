using Binarysharp.MemoryManagement;
using System;
using WireDog.Native;

namespace WireDog.SubRoutines
{
    // void __stdcall SetSocketKnown(SOCKET s);
    public class SetSocketKnown : AbstractSubRoutine
    {
        protected override int AllocationSize
        {
            get { return 50; }
        }

        public SetSocketKnown(MemorySharp memorySharp, IntPtr socketDescriptorsAddress)
        {
            var calloc = NativeMethods.GetProcAddressInModule("msvcrt.dll", "calloc");

            Code = new[]{           // stack: ret, s
                "push ebp",         // stack: ebp, ret, s
                "mov ebp, esp",
                "mov ebx, [ebp+8]", // s
                "mov ecx, " + socketDescriptorsAddress,
                "loopOverNodes:",
                "mov edx, [ecx]",
                "test edx, edx",
                "jz addValueAndReturn", // value is leeg?
                "mov ecx, [ecx+4]", // pointer naar volgende node
                "jmp loopOverNodes",

                "addValueAndReturn:",
                "push ecx", // malloc overschrijft ecx
                "push 4", // size
                "push 2", // aantal elementen
                // void* __cdecl calloc(size_t num, size_t size)
                "call " + calloc,
                "add esp, 8",
                "pop ecx",
                "mov [ecx], ebx",   // set value = s
                "add ecx, 4",
                "mov [ecx], eax",   // pointer naar volgende node
                "pop ebp",
                "ret 4"
            };

            Install(memorySharp);
        }
    }
}
