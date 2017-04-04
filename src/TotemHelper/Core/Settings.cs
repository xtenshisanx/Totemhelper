using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PoeHUD.Plugins;
using PoeHUD.Hud.Settings;
namespace TotemHelper.Core
{
    public class Settings : SettingsBase
    {
        [Menu("Totemradius")]
        public RangeNode<Int32> TotemRadius { get; set; }
        [Menu("Radiusdots")]
        public RangeNode<Int32> RadiusDots { get; set; }
        [Menu("Have to hold Key down")]
        public ToggleNode HoldKey { get; set; }
        public Settings()
        {
            Enable = true;

            TotemRadius = new RangeNode<int>(60, 10, 100);
            RadiusDots = new RangeNode<int>(360, 10, 360);
            HoldKey = new ToggleNode(true);
        }
    }
}
