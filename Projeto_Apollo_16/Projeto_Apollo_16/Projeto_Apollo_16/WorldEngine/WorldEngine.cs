using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto_Apollo_16
{
    public class WorldEngine
    {
        /* Fields */
        const int SECTOR_WIDTH = 2048;
        const int SECTOR_HEIGHT = 2048;


        /* Getters and Setters */
        public static int SectorWidth
        {
            get { return SECTOR_WIDTH; }
        }
        public static int SectorHeight
        {
            get { return SECTOR_HEIGHT; }
        }

        /* Constructor */
        public WorldEngine()
        {
        }
    }
}
