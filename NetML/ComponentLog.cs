namespace NetML
{
    public class ComponentLog
    {
        public LogLevel LoggingLevel;
        public LogModule LoggingModule;
    }

    public enum LogLevel
    {
        LOG_LEVEL_ERROR,
        LOG_LEVEL_WARN,
        LOG_LEVEL_DEBUG,
        LOG_LEVEL_INFO,
        LOG_LEVEL_FUNCTION,
        LOG_LEVEL_LOGIC,
        LOG_LEVEL_ALL,
        LOG_LEVEL_UNCOND,
    }

    public enum LogModule
    {
        NetmltoNs3,
        UdpEchoServerApplication,
        UdpEchoClientApplication,
        BulkSendApplication,
        PacketSink,
        GlobalRouteManagerImpl
    }
}
