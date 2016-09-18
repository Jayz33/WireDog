using System;
using System.Collections.Generic;
using System.Diagnostics;
using WireDog.Enums;

namespace WireDog.Delegation
{
    public delegate void SocketEventHandler(SocketEventType socketEventType, IntPtr socketEvent, int socketEventSize);
    public delegate void ProcessCheckedHandler(Process process, bool isChecked);
    public delegate void ProcessesRemovedHandler(IEnumerable<int> processIds);
}
