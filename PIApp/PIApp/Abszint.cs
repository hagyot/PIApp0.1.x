using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Abszint : Drink
    {
        public Abszint(string name, string vvpercent, bool custom)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Abszint";
            this.custom = custom;
    }
    }
}
