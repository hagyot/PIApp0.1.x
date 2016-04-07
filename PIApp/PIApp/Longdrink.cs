using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Longdrink : Drink
    {
        public Longdrink(string name, string vvpercent, bool custom)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Longdrink";
            this.custom = custom;
    }
    }
}
