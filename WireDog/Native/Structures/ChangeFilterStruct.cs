using System;
using System.Runtime.InteropServices;
using WireDog.Native.Enums;

namespace WireDog.Native.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ChangeFilterStruct
    {
        /// <summary>
        /// The size of the structure, in bytes. Must be set to sizeof(CHANGEFILTERSTRUCT), 
        /// otherwise the function fails with ERROR_INVALID_PARAMETER.
        /// </summary>
        public uint size;

        /// <summary>
        /// If the function succeeds, this field contains one of the following values, 
        /// <see cref="MessageFilterInfo"/>
        /// </summary>
        public MessageFilterInfo info;
    }
}
