using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Rum : Drink
    {
        public Rum(string name, string vvpercent, bool custom)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Rum";
            this.custom = custom;
    }
    }
}
