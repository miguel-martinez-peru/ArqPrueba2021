using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthZ.BackgroundTask.Config
{
    public class CustomBgSetting
    {
        public const string Name = "BgSetting";
        public BgSetting ServiceRoles { get; set; }
        public BgSetting ServiceAplicaciones { get; set; }
    }

    public class BgSetting
    {
        public string ExecTime { get; set; }
        public List<string> CodeSucces { get; set; }
    }
}
