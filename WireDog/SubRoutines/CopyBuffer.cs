using Binarysharp.MemoryManagement;

namespace WireDog.SubRoutines
{
    // void __stdcall CopyBuffer(void* source, void* destination, int len)
    public class CopyBuffer : AbstractSubRoutine
    {
        protected override int AllocationSize
        {
            get { return 150; }
        }

        public CopyBuffer(MemorySharp memorySharp)
        {
            Code = new[]{           // stack: ret, source, destination, len
                "push ebp",
                "mov ebp, esp",
                "mov edx, [ebp+8]", // source
                "mov ebx, [ebp+12]", // destination
                "mov ecx, [ebp+16]", // len
                "copyByte:",
                "test ecx, ecx",
                "jz cleanUpAndReturn",
                "mov al, byte [edx]",
                "mov [ebx], byte al",
                "inc ebx",
                "inc edx",
                "dec ecx",
                "jmp copyByte",
                "cleanUpAndReturn:",
                "pop ebp",
                "ret 12"
            };

            Install(memorySharp);
        }
    }
}
