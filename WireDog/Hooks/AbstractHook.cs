using Binarysharp.MemoryManagement;
using System;
using WireDog.Common;

namespace WireDog.Hooks
{
    public abstract class AbstractHook : AssemblerContainer
    {
        protected abstract string[] OriginalCode { get; }
        protected abstract IntPtr Address { get; }

        protected override void Install(MemorySharp memorySharp)
        {
            base.Install(memorySharp);
            memorySharp.Assembly.Inject("call " + CaveAddress, Address);
        }

        public void Remove(MemorySharp memorySharp)
        {
            memorySharp.Assembly.Inject(OriginalCode, Address);
        }
    }
}
