﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo_16_Shooter
{
    enum PacketTypes
    {
        SHOOTER_DATA,
        INPUT_DATA,
    }
    
    enum ConnectionID
    {
        SHOOTER,
    }

    public static class Globals
    {
        /* NETWORK RELATED INFORMATION */
        public const String      IP = "127.0.0.1";
        public const int         PORT = 14242;
        public const String      NETWORK_NAME = "apollo";

        // Graphics
        public const int SCREEN_RESOLUTION_WIDTH = 1024;
        public const int SCREEN_RESOLUTION_HEIGHT = 768;

        // Joystick
        public const int JOYSTICK_RANGE = 10000; //range do jogo pra pegar input
    }
}