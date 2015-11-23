using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Enums
{
    public class EnumItemName : Attribute
    {
        public string Name { get; set; }

        public EnumItemName(string name)
        {
            Name = name;
        }
    }
}
