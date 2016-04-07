using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Palinka : Drink
    {
        public Palinka(string name, string vvpercent, bool custom)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Pálinka";
            this.custom = custom;
    }
    }
}
