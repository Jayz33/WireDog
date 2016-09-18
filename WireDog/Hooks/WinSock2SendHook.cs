using Binarysharp.MemoryManagement;
using System;
using WireDog.Enums;
using WireDog.Native;

namespace WireDog.Hooks
{
    public class WinSock2SendHook : WinSock2Hook
    {
        protected override int AllocationSize
        {
            get { return 150; }
        }

        protected override string[] OriginalCode
        {
            get
            {
                return new[] { 
                    "mov edi, edi",
                    "push ebp",
                    "mov ebp, esp"
                };
            }
        }

        protected override IntPtr Address
        {
            get { return NativeMethods.GetProcAddress(WinSock2ModuleHandle, "send"); }
        }

        public WinSock2SendHook(MemorySharp memorySharp, IntPtr sendMessageDestinationHandle, IntPtr socketDescriptorsAddress)
            : base(memorySharp, sendMessageDestinationHandle, socketDescriptorsAddress)
        {
            Code = new[]{                       // stack: ret_ws32, ret_mainmodule, s, buf, len, flags  
                // backup registers
                "push ebp",
                "push edx",
                "push ecx",
                "push ebx",                     // stack: ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags  

                "mov edx, [esp+0x18]",
                "push edx",                     // stack: s, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags  
                "call " + IsSocketKnown,        // stack: ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "test eax, 1",
                "jnz continue",
                "mov edx, [esp+0x18]",
                "push edx",                     // stack: s, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags  
                "call " + SetSocketKnown,       // stack: ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "mov edx, [esp+0x18]",          // stack: s, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags  
                "push edx",
                "call " + SendSocketInfo,       // stack: ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags  
                "continue:",
                "mov ebx, [esp+0x18]",          // s
                "mov ecx, [esp+0x1C]",          // buf
                "mov edx, [esp+0x20]",          // len
                "push edx",                     // stack: len, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags        
                "push ecx",                     // stack: buf, len, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags  
                "push " + (int) SocketEventType.Send, // stack: msg, buf, len, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags  
                "push ebx",                     // stack: s, msg, buf, len, ebx, ecx, edx, ebp, ret_ws32, ret_mainmodule, s, buf, len, flags
                "call " + SendPacketData,

                // restore registers
                "pop ebx",
                "pop ecx",
                "pop edx",
                "pop ebp",                      // stack: ret_ws32, ret_mainmodule, s, buf, len, flags  

                "pop eax",                      // stack: ret_mainmodule, s, buf, len, flags  

                // originele code
                "mov edi, edi",                 
                "push ebp",                     // stack: ebp, ret_mainmodule, s, buf, len, flags 
                "mov ebp, esp",

                "push eax",                     // stack: ret_ws32, ebp, ret_mainmodule, s, buf, len, flags 
                "ret"
            };

            Install(memorySharp);
        }
    }
}
