using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class LevelItem
    {
        public LevelItemType type;
        public int location;
        public virtual string outputLine()
        {
            return string.Format("Location: {0} - {1}", location, type.type.ToFriendlyString());
        }
    }
}
