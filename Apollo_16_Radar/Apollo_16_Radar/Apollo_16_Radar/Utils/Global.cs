using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo_16_Radar
{
    enum PacketTypes
    {
        CONNECTION_ACCEPTED = 0,
        ID_PACKET,
        LOGIN,
        PILOT_DATA,
        INPUT_DATA,
        RADAR_DATA,
        RADAR_DATA_IMMEDIATE,
    }

    enum ConnectionID
    {
        PILOT = 0,
        RADAR
    }

    public static class Global
    {
        /* NETWORK RELATED INFORMATION */
        public const String      IP = "127.0.0.1";
        public const int         PORT = 14242;
        public const String      NETWORK_NAME = "apollo";
    }
}
