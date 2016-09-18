namespace WireDog.Native.Enums
{
    public enum ChangeWindowMessageFilterExAction
    {
        /// <summary>
        /// Resets the window message filter for hWnd to the default.
        /// Any message allowed globally or process-wide will get through,
        /// but any message not included in those two categories,
        /// and which comes from a lower privileged process, will be blocked.
        /// </summary>
        Reset = 0,

        /// <summary>
        /// Allows the message through the filter. 
        /// This enables the message to be received by hWnd, 
        /// regardless of the source of the message, 
        /// even it comes from a lower privileged process.
        /// </summary>
        Allow = 1,

        /// <summary>
        /// Blocks the message to be delivered to hWnd if it comes from
        /// a lower privileged process, unless the message is allowed process-wide 
        /// by using the ChangeWindowMessageFilter function or globally.
        /// </summary>
        DisAllow = 2
    }
}
