using Binarysharp.MemoryManagement;
using System;
using WireDog.Enums;
using WireDog.Native;
using WireDog.SubRoutines;

namespace WireDog.Hooks
{
    public class WinSock2RecvHook : WinSock2Hook
    {
        protected override string[] OriginalCode
        {
            get
            {
                return new[]{
                    "mov edi, edi",
                    "push ebp",
                    "mov ebp, esp"
                };
            }
        }

        protected override IntPtr Address
        {
            get { return NativeMethods.GetProcAddress(WinSock2ModuleHandle, "recv"); }
        }

        protected override int AllocationSize
        {
            get { return 150; }
        }

        public WinSock2RecvHook(MemorySharp memorySharp, IntPtr sendMessageDestinationHandle, IntPtr socketDescriptorsAddress)
            : base(memorySharp, sendMessageDestinationHandle, socketDescriptorsAddress)
        {
            var recvCallback = new RecvCallback(memorySharp, IsSocketKnown, SetSocketKnown, SendSocketInfo, SendPacketData);

            Code = new[]{                       // stack: ret_ws32, ret_mainmodule, s, buf, len, flags
                // backup registers
                "push ebp",
                "push edx",
                "push ecx",
                "push ebx",                     // stack: ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags

                // repush parameters
                "push dword [esp+36]",          // stack: flags, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "push dword [esp+36]",          // stack: len, flags, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "push dword [esp+36]",          // stack: buf, len, flags, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "push dword [esp+36]",          // stack: s, buf, len, flags, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                // push callback address
                "push " + recvCallback,         // stack: ret_callback, s, buf, len, flags, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                // store ret_ws32
                "mov eax, [esp+36]",
                // original code
                "mov edi, edi",
                "push ebp",                     // stack: ebp, ret_callback, s, buf, len, flags, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "mov ebp, esp",                
                // push ret_ws32
                "push eax",                     // stack: ret_ws32, ebp, ret_callback, s, buf, len, flags, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "ret",
            };

            Install(memorySharp);
        }
    }
}
