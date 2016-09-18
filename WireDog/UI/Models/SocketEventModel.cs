using System;
using WireDog.Enums;

namespace WireDog.UI.Models
{
    public class SocketEventModel
    {
        public SocketEventType SocketEventType { get; set; }
        public DateTime Timestamp { get; set; }
        public string ProcessName { get; set; }
        public string LocalAddress { get; set; }
        public string RemoteAddress { get; set; }
        public int Size { get; set; }
        public byte[] Data { get; set; }
    }
}
