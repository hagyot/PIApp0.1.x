using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Tequila : Drink
    {
        public Tequila(string name, string vvpercent, bool custom)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Tequila";
            this.custom = custom;
    }
    }
}
