using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class SoundItem : LevelItem
    {
        public byte[] soundbytes;
        public override string outputLine()
        {
            return string.Format("Location: {0} - {1} - {2}", location, type.type.ToFriendlyString(), soundbytes);
        }
    }
}
