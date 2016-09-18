using Binarysharp.MemoryManagement;
using System;

namespace WireDog.Common
{
    public abstract class AssemblerContainer
    {
        protected abstract int AllocationSize { get; }
        protected string[] Code { get; set; }
        public IntPtr CaveAddress { get; set; }

        protected virtual void Install(MemorySharp memorySharp)
        {
            var cave = memorySharp.Memory.Allocate(AllocationSize);
            CaveAddress = cave.BaseAddress;
            memorySharp.Assembly.Inject(Code, CaveAddress);
        }
    }
}
