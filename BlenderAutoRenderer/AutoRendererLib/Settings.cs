using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlenderAutoRenderer
{
    public class Settings
    {
        public string ProgramLocation { get; set; }

        public Settings()
        {
        }

        public Settings (string programLocation)
        {
            ProgramLocation = programLocation;
        }
    }
}
