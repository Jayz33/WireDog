using Binarysharp.MemoryManagement;
using WireDog.Native;

namespace WireDog.SubRoutines
{
    // void __stdcall GetSocketInfo(SOCKET s, LocalAndRemoteSockAddr* localAndRemoteSockAddr);
    public class GetSocketInfo : AbstractSubRoutine
    {
        protected override int AllocationSize
        {
            get { return 200; }
        }

        public GetSocketInfo(MemorySharp memorySharp)
        {
            var ws2Handle = NativeMethods.GetModuleHandle("ws2_32.dll");
            var getSockName = NativeMethods.GetProcAddress(ws2Handle, "getsockname");
            var getPeerName = NativeMethods.GetProcAddress(ws2Handle, "getpeername");

            Code = new[]{           // stack: ret, s, localAndRemoteSockAddr
                "sub esp, 4",
                "mov eax, 1",
                "mov edx, [esp+12]",// localAndRemoteSockAddr deel 1
                "getSockAddrInfo:",
                "mov ebx, [esp+8]", // SOCKET s
                "mov ecx, esp",
                "mov dword [ecx], 0x10",    // sizeof(SOCKADDR)
                "push ecx",         // stack: &sizeof(SOCKADDR)
                "push edx",         // stack: &localAndRemoteSockAddr, &sizeof(SOCKADDR)
                "push ebx",         // stack: s, &localAndRemoteSockAddr, &sizeof(SOCKADDR)
                "test eax, eax",
                "jz getRemoteInfo",
                "call " + getSockName,
                "mov eax, 0",
                "mov edx, [esp+12]",
                "add edx, 16",      // localAndRemoteSockAddr deel 2
                "jmp getSockAddrInfo",
                "getRemoteInfo:",
                "call " + getPeerName,
                "add esp, 4",       // stack: ret, s, localAndRemoteSockAddr
                "ret 8"
            };

            Install(memorySharp);
        }
    }
}
