using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DataContent
{
    public class XML_TitleScreenData
    {
        public string background_assetName { get; set; }
        public int background_positionX { get; set; }
        public int background_positionY { get; set; }
        public string title_assetName { get; set; }
        public int title_positionX { get; set; }
        public int title_positionY { get; set; }
        public string start_text { get; set; }
        public int start_positionX { get; set; }
        public int start_positionY { get; set; }
        public int start_color_red { get; set; }
        public int start_color_green { get; set; }
        public int start_color_blue { get; set; }
    }
}
