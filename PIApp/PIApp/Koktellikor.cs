using System;
using System.Collections.Generic;
using System.Text;

namespace PIApp
{
    class Koktellikor: Drink
    {
        public Koktellikor(string name, string vvpercent, bool custom)
        {
            this.name = name;
            this.vvpercent = vvpercent;
            this.type = "Koktellikor";
            this.custom = custom;
    }
    }
}
