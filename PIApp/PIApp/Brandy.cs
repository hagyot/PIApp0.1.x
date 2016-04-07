using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Brandy : Drink
    {
        public Brandy(string name, string vvpercent, bool custom)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Brandy";
            this.custom = custom;
    }
    }
}
