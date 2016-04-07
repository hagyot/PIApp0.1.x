using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Ciderradler : Drink
    {
        public Ciderradler(string name, string vvpercent, bool custom)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Cider, Radler";
            this.custom = custom;
    }
    }
}
